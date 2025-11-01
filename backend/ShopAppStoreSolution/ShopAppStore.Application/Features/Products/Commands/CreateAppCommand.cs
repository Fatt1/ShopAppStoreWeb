using ShopAppStore.Application.Abstractions.CQRS.Command;
using ShopAppStore.Application.Features.Medias.DTOs;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Application.Services.Interfaces;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Shared;

namespace ShopAppStore.Application.Features.Products.Commands
{
    public class CreateAppCommand : ICommand<CreateAppDTO>
    {
        public string AppName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ThumbnailUrl { get; set; } = null!;

        public string ThumbnailPublicId { get; set; } = null!;

        public Guid DurationId { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Status { get; set; } = null!;

        public Guid? BlogId { get; set; }

        public string? Note { get; set; }
        public ICollection<CreateAttributeAppDTO> AttributeApps { get; set; } = new List<CreateAttributeAppDTO>();

        public List<Guid> Categories { get; set; } = new List<Guid>();
        public List<ProductImageInfo> ProductImages { get; set; } = new List<ProductImageInfo>();

    }
    public class CreateAppCommandHandler : ICommandHandler<CreateAppCommand, CreateAppDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppRepository _appRepository;
        private readonly IImageUploadService _imageUploadService;
        private readonly ISlugGenerator _slugGenerator;

        public CreateAppCommandHandler(
            IUnitOfWork unitOfWork,
            IAppRepository appRepository,
            IImageUploadService imageUploadService,
            ISlugGenerator slugGenerator)
        {
            _unitOfWork = unitOfWork;
            _appRepository = appRepository;
            _imageUploadService = imageUploadService;
            _slugGenerator = slugGenerator;
        }

        public async Task<Result<CreateAppDTO>> Handle(CreateAppCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Bắt đầu transaction
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                // Tạo slug nếu chưa có
                var slug = string.IsNullOrWhiteSpace(request.Slug)
                    ? _slugGenerator.GenerateSlug(request.AppName)
                    : request.Slug;

                // Kiểm tra slug có unique không
                var isSlugUnique = await _appRepository.IsSlugUniqueAsync(slug, cancellationToken);
                if (!isSlugUnique)
                {
                    // Rollback hình ảnh nếu slug đã tồn tại
                    await RollbackImagesAsync(request.ThumbnailPublicId, request.ProductImages);
                    return Result<CreateAppDTO>.Failure(
                        new Error("App.SlugExists", $"Slug '{slug}' đã tồn tại."));
                }

                // Tạo App entity
                var app = new App
                {
                    Id = Guid.NewGuid(),
                    AppName = request.AppName,
                    Description = request.Description,
                    ThumbnailUrl = request.ThumbnailUrl,
                    DurationId = request.DurationId,
                    OriginalPrice = request.OriginalPrice,
                    CurrentPrice = request.CurrentPrice,
                    Slug = slug,
                    Status = request.Status,
                    BlogId = request.BlogId,
                    Note = request.Note,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    IsDeleted = false,
                    SoldCount = 0
                };

                // Thêm Categories
                if (request.Categories.Any())
                {
                    app.AppCategories = request.Categories.Select(categoryId => new AppCategory
                    {
                        Id = Guid.NewGuid(),
                        AppId = app.Id,
                        CategoryId = categoryId
                    }).ToList();
                }

                // Thêm AttributeApps
                if (request.AttributeApps.Any())
                {
                    app.AttributeApps = request.AttributeApps.Select(attr => new AttributeApp
                    {
                        Id = Guid.NewGuid(),
                        AppId = app.Id,
                        AttributeName = attr.AttributeName,
                        AttributeValue = attr.AttributeValue
                    }).ToList();
                }

                // Thêm AppImages
                if (request.ProductImages.Any())
                {
                    app.AppImages = request.ProductImages.Select(img => new AppImage
                    {
                        Id = Guid.NewGuid(),
                        AppId = app.Id,
                        PublicId = img.PublicId,
                        ImageUrl = img.Url
                    }).ToList();
                }

                // Thêm App vào database
                await _appRepository.AddAsync(app, cancellationToken);

                // Lưu thay đổi
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult <= 0)
                {
                    // Rollback transaction và xóa hình ảnh
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    await RollbackImagesAsync(request.ThumbnailPublicId, request.ProductImages);
                    return Result<CreateAppDTO>.Failure(
                        new Error("App.CreateFailed", "Không thể tạo sản phẩm."));
                }

                // Commit transaction
                await _unitOfWork.CommitAsync(cancellationToken);

                return Result<CreateAppDTO>.Success(new CreateAppDTO { Id = app.Id });
            }
            catch (Exception ex)
            {
                // Rollback transaction và xóa hình ảnh khi có lỗi
                await _unitOfWork.RollbackAsync(cancellationToken);
                await RollbackImagesAsync(request.ThumbnailPublicId, request.ProductImages);

                return Result<CreateAppDTO>.Failure(
                    new Error("App.CreateException", $"Lỗi khi tạo sản phẩm: {ex.Message}"));
            }
        }

        /// <summary>
        /// Xóa tất cả hình ảnh đã upload khi quá trình tạo sản phẩm thất bại
        /// </summary>
        private async Task RollbackImagesAsync(string thumbnailPublicId, List<ProductImageInfo> productImages)
        {
            try
            {
                // Xóa thumbnail
                if (!string.IsNullOrWhiteSpace(thumbnailPublicId))
                {
                    await _imageUploadService.DeleteImageAsync(thumbnailPublicId);
                }

                // Xóa tất cả product images
                if (productImages.Any())
                {
                    foreach (var image in productImages)
                    {
                        if (!string.IsNullOrWhiteSpace(image.PublicId))
                        {
                            await _imageUploadService.DeleteImageAsync(image.PublicId);
                        }
                    }
                }
            }
            catch
            {
                // Log lỗi nếu cần, nhưng không throw exception
                // vì đây là quá trình cleanup
            }
        }
    }
}
