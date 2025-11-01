using Microsoft.Extensions.Configuration;
using ShopAppStore.Application.Services.Interfaces;
using System.Security.Cryptography;

namespace ShopAppStore.Infrastructure.Services.Encryption
{
    public class AesEncryptionService : IEncryptionService
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public AesEncryptionService(IConfiguration configuration)
        {
            // Lấy key và IV từ configuration (appsettings.json)
            // Key phải có độ dài 32 bytes (256 bits) cho AES-256
            var keyString = configuration["AES_KEY"]
                ?? throw new InvalidOperationException("AES encryption key is not configured");
            var ivString = configuration["AES_IV"]
                ?? throw new InvalidOperationException("AES encryption IV is not configured");

            _key = Convert.FromBase64String(keyString);
            _iv = Convert.FromBase64String(ivString);

            // Validate key và IV length
            if (_key.Length != 32)
                throw new InvalidOperationException("AES key must be 32 bytes (256 bits)");
            if (_iv.Length != 16)
                throw new InvalidOperationException("AES IV must be 16 bytes (128 bits)");
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            var buffer = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var msDecrypt = new MemoryStream(buffer);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}