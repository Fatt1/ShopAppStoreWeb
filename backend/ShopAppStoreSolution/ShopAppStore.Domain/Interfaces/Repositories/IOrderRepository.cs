using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order, Guid>
    {
        /// <summary>
        /// Lấy Order với OrderItems
        /// </summary>
        Task<Order?> GetOrderWithItemsAsync(Guid orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kiểm tra xem app có trong order không
        /// </summary>
        Task<bool> IsAppInOrderAsync(Guid orderId, Guid appId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kiểm tra order có thuộc về user không
        /// </summary>
        Task<bool> IsOrderOwnedByUserAsync(Guid orderId, string userId, CancellationToken cancellationToken = default);
    }
}
