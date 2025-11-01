using ShopAppStore.Application.Abstractions.CQRS.Command;
using ShopAppStore.Application.Services.Interfaces;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Shared;

namespace ShopAppStore.Application.Features.Combos.Commands.CreateCombo
{
    public class CreateComboCommand : ICommand<Guid>
    {
        public string ComboName { get; set; } = null!;
        public string? Description { get; set; }
        public Guid ComboTypeId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string PublicIdImage { get; set; } = null!;
        public decimal ComboPrice { get; set; }
        public List<Guid> AppIds { get; set; } = new List<Guid>();
    }

    public class CreateComboCommandHandler : ICommandHandler<CreateComboCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComboRepository _comboRepository;
        private readonly IComboTypeRepository _comboTypeRepository;
        private readonly IAppRepository _appRepository;
        private readonly IImageUploadService _imageUploadService;

        public CreateComboCommandHandler(
            IUnitOfWork unitOfWork,
            IComboRepository comboRepository,
            IComboTypeRepository comboTypeRepository,
            IAppRepository appRepository,
            IImageUploadService imageUploadService)
        {
            _unitOfWork = unitOfWork;
            _comboRepository = comboRepository;
            _comboTypeRepository = comboTypeRepository;
            _appRepository = appRepository;
            _imageUploadService = imageUploadService;
        }

        public async Task<Result<Guid>> Handle(CreateComboCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Validate basic inputs
                if (string.IsNullOrWhiteSpace(request.ComboName))
                {
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("Combo.NameRequired", "Tên combo không được để trống."));
                }

                if (request.ComboPrice <= 0)
                {
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("Combo.InvalidPrice", "Giá combo phải lớn hơn 0."));
                }

                if (!request.AppIds.Any())
                {
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("Combo.NoApps", "Combo phải có ít nhất 1 sản phẩm."));
                }

                // 2. Kiểm tra ComboType có tồn tại không
                var comboType = await _comboTypeRepository.GetByIdAsync(request.ComboTypeId, cancellationToken);

                if (comboType is null)
                {
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("ComboType.NotFound", "Loại combo không tồn tại."));
                }

                // 3. Kiểm tra số lượng AppIds có khớp với QuantityRequired của ComboType không
                if (request.AppIds.Count != comboType.QuantityRequired)
                {
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("Combo.InvalidQuantity",
                            $"Combo loại '{comboType.Name}' yêu cầu đúng {comboType.QuantityRequired} sản phẩm, " +
                            $"nhưng bạn đã chọn {request.AppIds.Count} sản phẩm."));
                }

                // 4. Kiểm tra duplicate AppIds
                var distinctAppIds = request.AppIds.Distinct().ToList();
                if (distinctAppIds.Count != request.AppIds.Count)
                {
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("Combo.DuplicateApps", "Không được chọn trùng sản phẩm trong combo."));
                }

                // 5. Kiểm tra tất cả AppIds có tồn tại và chưa bị xóa không
                var existingAppIds = await _appRepository.GetExistingAppIdsAsync(distinctAppIds, cancellationToken);

                if (existingAppIds.Count != distinctAppIds.Count)
                {
                    var missingAppIds = distinctAppIds.Except(existingAppIds).ToList();
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("Combo.AppsNotFound",
                            $"Các sản phẩm sau không tồn tại hoặc đã bị xóa: {string.Join(", ", missingAppIds)}"));
                }

                // 6. Kiểm tra tồn kho của tất cả apps
                var appStocks = await _appRepository.GetAppStocksAsync(distinctAppIds, cancellationToken);

                var appsWithNoStock = appStocks.Where(x => x.Value == 0).ToList();
                if (appsWithNoStock.Any())
                {
                    var noStockAppIds = string.Join(", ", appsWithNoStock.Select(x => x.Key));
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("Combo.AppsOutOfStock",
                            $"Các sản phẩm sau đã hết hàng (stock = 0): {noStockAppIds}. " +
                            $"Không thể tạo combo với sản phẩm hết hàng."));
                }

                // 7. Bắt đầu transaction
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                // 8. Tạo Combo entity
                var combo = new Combo
                {
                    Id = Guid.NewGuid(),
                    ComboName = request.ComboName,
                    Description = request.Description,
                    ComboTypeId = request.ComboTypeId,
                    ImageUrl = request.ImageUrl,
                    PublicIdImage = request.PublicIdImage,
                    ComboPrice = request.ComboPrice,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                // 9. Tạo ComboApp relationships
                combo.ComboApps = distinctAppIds.Select(appId => new ComboApp
                {
                    Id = Guid.NewGuid(),
                    ComboId = combo.Id,
                    AppId = appId
                }).ToList();

                // 10. Thêm Combo vào database
                await _comboRepository.AddAsync(combo, cancellationToken);

                // 11. Lưu thay đổi
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult <= 0)
                {
                    // Rollback transaction và xóa hình ảnh
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    await RollbackImageAsync(request.PublicIdImage);
                    return Result<Guid>.Failure(
                        new Error("Combo.CreateFailed", "Không thể tạo combo."));
                }

                // 12. Commit transaction
                await _unitOfWork.CommitAsync(cancellationToken);

                return Result<Guid>.Success(combo.Id);
            }
            catch (Exception ex)
            {
                // Rollback transaction và xóa hình ảnh khi có lỗi
                await _unitOfWork.RollbackAsync(cancellationToken);
                await RollbackImageAsync(request.PublicIdImage);

                return Result<Guid>.Failure(
                    new Error("Combo.Exception", $"Lỗi khi tạo combo: {ex.Message}"));
            }
        }

        /// <summary>
        /// Xóa hình ảnh đã upload khi quá trình tạo combo thất bại
        /// </summary>
        private async Task RollbackImageAsync(string publicIdImage)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(publicIdImage))
                {
                    await _imageUploadService.DeleteImageAsync(publicIdImage);
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