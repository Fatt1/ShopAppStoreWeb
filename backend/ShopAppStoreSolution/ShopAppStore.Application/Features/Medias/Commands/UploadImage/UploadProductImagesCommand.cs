using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShopAppStore.Application.Abstractions.CQRS.Command;
using ShopAppStore.Application.Features.Medias.DTOs;
using ShopAppStore.Application.Services.Interfaces;
using ShopAppStore.Shared;

namespace ShopAppStore.Application.Features.Medias.Commands.UploadImage
{
    public record UploadProductImagesCommand : ICommand<UploadProductImagesDTO>
    {
        public IFormFile ThumbnailFile { get; set; } = null!;
        public ICollection<IFormFile> AppImages { get; set; } = new List<IFormFile>();
    }
    public class UploadProductImagesCommandHandler : ICommandHandler<UploadProductImagesCommand, UploadProductImagesDTO>
    {
        private readonly ILogger<UploadProductImagesCommandHandler> _logger;
        private readonly IImageUploadService _imageUploadService;

        public UploadProductImagesCommandHandler(
            ILogger<UploadProductImagesCommandHandler> logger,
            IImageUploadService imageUploadService)
        {
            _logger = logger;
            _imageUploadService = imageUploadService;
        }

        public async Task<Result<UploadProductImagesDTO>> Handle(UploadProductImagesCommand request, CancellationToken cancellationToken)
        {
            var uploadedImages = new List<UploadedImageInfo>();

            try
            {
                // Upload thumbnail
                using (var thumbnailStream = request.ThumbnailFile.OpenReadStream())
                {
                    var thumbnailResult = await _imageUploadService.UploadImageAsync(
                        thumbnailStream,
                        request.ThumbnailFile.FileName,
                        "products/thumbnails");

                    if (!string.IsNullOrEmpty(thumbnailResult.Error))
                    {
                        return Result<UploadProductImagesDTO>.Failure(
                            new Error("Upload.ThumbnailFailed", thumbnailResult.Error));
                    }

                    uploadedImages.Add(new UploadedImageInfo
                    {
                        PublicId = thumbnailResult.PublicId,
                        Url = thumbnailResult.Url,
                        ImageType = "Thumbnail"
                    });

                    _logger.LogInformation("Uploaded thumbnail: {PublicId}", thumbnailResult.PublicId);
                }

                // Upload product images
                foreach (var imageFile in request.AppImages)
                {
                    using var imageStream = imageFile.OpenReadStream();
                    var imageResult = await _imageUploadService.UploadImageAsync(
                        imageStream,
                        imageFile.FileName,
                        "products/images");

                    if (!string.IsNullOrEmpty(imageResult.Error))
                    {
                        // Rollback: Delete all uploaded images
                        await RollbackUploadedImages(uploadedImages.Select(x => x.PublicId).ToList());

                        return Result<UploadProductImagesDTO>.Failure(
                            new Error("Upload.ImageFailed", imageResult.Error));
                    }

                    uploadedImages.Add(new UploadedImageInfo
                    {
                        PublicId = imageResult.PublicId,
                        Url = imageResult.Url,
                        ImageType = "ProductImage"
                    });

                    _logger.LogInformation("Uploaded product image: {PublicId}", imageResult.PublicId);
                }

                var result = new UploadProductImagesDTO
                {
                    ThumbnailPublicId = uploadedImages.First(x => x.ImageType == "Thumbnail").PublicId,
                    ThumbnailUrl = uploadedImages.First(x => x.ImageType == "Thumbnail").Url,
                    AppImages = uploadedImages.Where(x => x.ImageType == "ProductImage")
                        .Select(x => new ProductImageInfo
                        {
                            PublicId = x.PublicId,
                            Url = x.Url
                        }).ToList()
                };

                _logger.LogInformation("Successfully uploaded {Count} images", uploadedImages.Count);

                return Result<UploadProductImagesDTO>.Success(result);
            }
            catch (Exception ex)
            {
                // Rollback all uploaded images
                await RollbackUploadedImages(uploadedImages.Select(x => x.PublicId).ToList());

                _logger.LogError(ex, "Failed to upload images");
                return Result<UploadProductImagesDTO>.Failure(
                    new Error("Upload.Failed", ex.Message));
            }
        }

        private async Task RollbackUploadedImages(List<string> publicIds)
        {
            foreach (var publicId in publicIds)
            {
                try
                {
                    await _imageUploadService.DeleteImageAsync(publicId);
                    _logger.LogInformation("Deleted image during rollback: {PublicId}", publicId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to delete image during rollback: {PublicId}", publicId);
                }
            }
        }
    }
    // Helper classes
    public class UploadedImageInfo
    {
        public string PublicId { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string ImageType { get; set; } = null!;
    }
}
