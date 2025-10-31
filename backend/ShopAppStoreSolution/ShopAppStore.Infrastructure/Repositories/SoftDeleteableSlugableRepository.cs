using Microsoft.EntityFrameworkCore;
using ShopAppStore.Domain.Common;
using ShopAppStore.Domain.Interfaces;
using ShopAppStore.Domain.Interfaces.Repositories;

namespace ShopAppStore.Infrastructure.Repositories
{
    // Lớp trừu tượng mới kết hợp cả hai
    public abstract class SoftDeleteableSlugableRepository<TEntity, TKey>
        : SoftDeleteableRepository<TEntity, TKey>, // Kế thừa từ 1 class
          ISlugableRepository<TEntity, TKey>       // Implement interface còn lại
        where TEntity : class, IEntity<TKey>, ISoftDelete, IHasSlug // Gộp cả 2 ràng buộc
    {
        protected SoftDeleteableSlugableRepository(AppDbContext context) : base(context)
        {
        }

        public Task<TEntity?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {

            return _dbSet.FirstOrDefaultAsync(entity => entity.Slug == slug, cancellationToken);
        }

        public Task<bool> IsSlugUniqueAsync(string slug, CancellationToken cancellationToken = default)
        {

            return _dbSet.AsNoTracking().AnyAsync(entity => entity.Slug == slug, cancellationToken);
        }
    }
}
