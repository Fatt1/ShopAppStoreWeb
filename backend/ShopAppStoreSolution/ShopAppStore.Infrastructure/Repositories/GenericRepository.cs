using Microsoft.EntityFrameworkCore;
using ShopAppStore.Domain.Interfaces;
using ShopAppStore.Domain.Interfaces.Repositories;

namespace ShopAppStore.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;

            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.AsNoTracking().AnyAsync(entity => entity.Id!.Equals(id), cancellationToken);
            return entity;
        }
    }
}
