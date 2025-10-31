using Microsoft.EntityFrameworkCore;
using ShopAppStore.Domain.Common;
using ShopAppStore.Domain.Interfaces;
using ShopAppStore.Domain.Interfaces.Repositories;

namespace ShopAppStore.Infrastructure.Repositories
{
    public abstract class SlugableRepository<TEntity, TKey> : GenericRepository<TEntity, TKey>, ISlugableRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, IHasSlug
    {
        protected SlugableRepository(AppDbContext context) : base(context)
        {
        }

        public Task<TEntity?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return _dbSet.FirstOrDefaultAsync(entity => entity.Slug == slug, cancellationToken);
        }

        public async Task<bool> IsSlugUniqueAsync(string slug, CancellationToken cancellationToken = default)
        {
            // 1. Kiểm tra xem có BẤT KỲ entity nào CÓ slug này không
            bool slugExists = await _dbSet.AsNoTracking()
                                          .AnyAsync(entity => entity.Slug == slug, cancellationToken);
            // 2. Trả về kết quả ngược lại (nếu nó tồn tại, thì nó không unique)
            return !slugExists;
        }


    }
}
