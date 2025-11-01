using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using ShopAppStore.Application.Models;
using ShopAppStore.Application.Services.Interfaces;

namespace ShopAppStore.Infrastructure.Services.UploadImage
{
    public class CloudinaryService : IImageUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cloudinary = new Cloudinary(configuration["CLOUDINARY_URL"]);
            _cloudinary.Api.Secure = true; // Cấu hình bảo mật HTTPS
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
                return false;

            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }

        public async Task<ImageUploadResultModel> UploadImageAsync(Stream fileStream, string fileName, string folderName)
        {
            Cloudinary cloudinary = new Cloudinary(_configuration["CLOUDINARY_URL"]);
            cloudinary.Api.Secure = true;

            var uploadParams = new ImageUploadParams()
            {
                PublicId = $"{folderName}/{Path.GetFileNameWithoutExtension(fileName)}-{Guid.NewGuid()}",
                File = new FileDescription(fileName, fileStream),
                Overwrite = true
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            return new ImageUploadResultModel
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl?.ToString() ?? string.Empty,
                Error = uploadResult.Error?.Message ?? string.Empty

            };
        }
    }
}
