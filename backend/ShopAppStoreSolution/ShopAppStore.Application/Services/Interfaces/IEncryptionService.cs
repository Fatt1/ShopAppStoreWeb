namespace ShopAppStore.Application.Services.Interfaces
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Mã hóa chuỗi bằng AES
        /// </summary>
        string Encrypt(string plainText);

        /// <summary>
        /// Giải mã chuỗi AES
        /// </summary>
        string Decrypt(string cipherText);
    }
}
