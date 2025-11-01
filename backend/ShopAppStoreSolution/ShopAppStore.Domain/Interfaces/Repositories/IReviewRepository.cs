using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface IReviewRepository : IGenericRepository<Review, Guid>
    {
        /// <summary>
        /// Kiểm tra xem user đã review app trong order này chưa
        /// </summary>
        Task<bool> HasUserReviewedAppInOrderAsync(Guid appId, Guid orderId, string userId, CancellationToken cancellationToken = default);
    }
}
