using ShopAppStore.Domain.Common;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface ISlugableRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : IEntity<TKey>, IHasSlug
    {
        Task<TEntity?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<bool> IsSlugUniqueAsync(string slug, CancellationToken cancellationToken = default);
    }
}

