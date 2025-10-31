using Microsoft.EntityFrameworkCore;
using ShopAppStore.Domain.Common;
using ShopAppStore.Domain.Interfaces;
using ShopAppStore.Domain.Interfaces.Repositories;

namespace ShopAppStore.Infrastructure.Repositories
{
    public abstract class SoftDeleteableRepository<TEntity, TKey> : GenericRepository<TEntity, TKey>, ISoftDeleteableRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, ISoftDelete
    {
        protected SoftDeleteableRepository(AppDbContext context) : base(context)
        {
        }

        public async Task SoftDeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(entity => entity.Id!.Equals(id));
            entity!.IsDeleted = true;
        }
    }
}
