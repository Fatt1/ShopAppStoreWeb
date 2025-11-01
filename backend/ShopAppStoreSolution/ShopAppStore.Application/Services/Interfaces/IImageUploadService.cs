using ShopAppStore.Application.Models;

namespace ShopAppStore.Application.Services.Interfaces
{
    public interface IImageUploadService
    {
        Task<ImageUploadResultModel> UploadImageAsync(Stream fileStream, string fileName, string folderName);

        Task<bool> DeleteImageAsync(string publicId);
    }
}
