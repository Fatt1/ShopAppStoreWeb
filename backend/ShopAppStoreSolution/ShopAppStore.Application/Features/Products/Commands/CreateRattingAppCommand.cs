using ShopAppStore.Application.Abstractions.CQRS.Command;
using ShopAppStore.Domain.Constants;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Shared;

namespace ShopAppStore.Application.Features.Products.Commands
{
    public record CreateRattingAppCommand : ICommand<Guid>
    {
        public Guid AppId { get; init; }
        public Guid UserId { get; init; }
        public Guid OrderId { get; init; }
        public int Rating { get; init; }
        public string Comment { get; init; } = null!;
    }

    public class CreateRattingAppCommandHandler : ICommandHandler<CreateRattingAppCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewRepository _reviewRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAppRepository _appRepository;
        public CreateRattingAppCommandHandler(
            IUnitOfWork unitOfWork,
            IReviewRepository reviewRepository,
            IOrderRepository orderRepository,
            IAppRepository appRepository)
        {
            _unitOfWork = unitOfWork;
            _reviewRepository = reviewRepository;
            _orderRepository = orderRepository;
            _appRepository = appRepository;
        }
        public async Task<Result<Guid>> Handle(CreateRattingAppCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate Rating (phải từ 1-5)
            if (request.Rating < 1 || request.Rating > 5)
            {
                return Result<Guid>.Failure(
                    new Error("Review.InvalidRating", "Rating phải từ 1 đến 5 sao."));
            }

            // 2. Kiểm tra Order có tồn tại và lấy thông tin
            var order = await _orderRepository.GetOrderWithItemsAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return Result<Guid>.Failure(
                    new Error("Order.NotFound", "Đơn hàng không tồn tại."));
            }

            // 3. Kiểm tra Order có thuộc về User này không
            var isOrderOwnedByUser = await _orderRepository.IsOrderOwnedByUserAsync(
                request.OrderId,
                request.UserId.ToString(),
                cancellationToken);

            if (!isOrderOwnedByUser)
            {
                return Result<Guid>.Failure(
                    new Error("Review.Unauthorized", "Bạn không có quyền đánh giá đơn hàng này."));
            }

            // 4. Kiểm tra Order đã hoàn thành chưa
            if (order.OrderStatus != OrderStatus.Completed)
            {
                return Result<Guid>.Failure(
                    new Error("Review.OrderNotCompleted", "Chỉ có thể đánh giá đơn hàng đã hoàn thành."));
            }

            // 5. Kiểm tra App có trong Order không
            var isAppInOrder = await _orderRepository.IsAppInOrderAsync(
                request.OrderId,
                request.AppId,
                cancellationToken);

            if (!isAppInOrder)
            {
                return Result<Guid>.Failure(
                    new Error("Review.AppNotInOrder", "Sản phẩm không có trong đơn hàng này."));
            }

            // 6. Kiểm tra App có tồn tại không
            var appExists = await _appRepository.ExistsAsync(request.AppId, cancellationToken);

            if (!appExists)
            {
                return Result<Guid>.Failure(
                    new Error("App.NotFound", "Sản phẩm không tồn tại."));
            }

            // 7. Kiểm tra User đã review App trong Order này chưa
            var hasReviewed = await _reviewRepository.HasUserReviewedAppInOrderAsync(
                request.AppId,
                request.OrderId,
                request.UserId.ToString(),
                cancellationToken);

            if (hasReviewed)
            {
                return Result<Guid>.Failure(
                    new Error("Review.AlreadyExists", "Bạn đã đánh giá sản phẩm này trong đơn hàng này rồi."));
            }

            // 8. Tạo Review mới
            var review = new Review
            {
                Id = Guid.NewGuid(),
                AppId = request.AppId,
                UserId = request.UserId.ToString(),
                OrderId = request.OrderId,
                Rating = request.Rating,
                Comment = request.Comment,
                ReviewStatus = ReviewStatus.Public,
                CreateAt = DateTime.UtcNow,
                AdminReply = string.Empty,
                AdminReplyAt = default,
                AdminReplyById = null
            };

            try
            {
                // 9. Thêm Review vào database
                await _reviewRepository.AddAsync(review, cancellationToken);

                // 10. Lưu thay đổi
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult <= 0)
                {
                    return Result<Guid>.Failure(
                        new Error("Review.CreateFailed", "Không thể tạo đánh giá."));
                }

                return Result<Guid>.Success(review.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(
                    new Error("Review.Exception", $"Lỗi khi tạo đánh giá: {ex.Message}"));
            }
        }
    }
}
