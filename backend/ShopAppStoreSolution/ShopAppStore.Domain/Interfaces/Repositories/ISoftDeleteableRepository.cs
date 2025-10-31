using ShopAppStore.Domain.Common;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface ISoftDeleteableRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : IEntity<TKey>, ISoftDelete
    {
        Task SoftDeleteAsync(TKey id, CancellationToken cancellationToken = default);


    }

}
