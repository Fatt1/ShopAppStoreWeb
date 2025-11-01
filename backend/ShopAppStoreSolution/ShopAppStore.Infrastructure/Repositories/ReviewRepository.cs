using Microsoft.EntityFrameworkCore;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review, Guid>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> HasUserReviewedAppInOrderAsync(Guid appId, Guid orderId, string userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(r => r.AppId == appId
                    && r.OrderId == orderId
                    && r.UserId == userId,
                    cancellationToken);
        }
    }
}
