using Microsoft.EntityFrameworkCore;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Order?> GetOrderWithItemsAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }

        public async Task<bool> IsAppInOrderAsync(Guid orderId, Guid appId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(o => o.Id == orderId)
                .SelectMany(o => o.OrderItems)
                .AnyAsync(oi => oi.AppId == appId, cancellationToken);
        }

        public async Task<bool> IsOrderOwnedByUserAsync(Guid orderId, string userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(o => o.Id == orderId && o.UserId == userId, cancellationToken);
        }
    }
}
