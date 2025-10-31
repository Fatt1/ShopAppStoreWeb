namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : IEntity<Tkey>
    {
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Tkey id, CancellationToken cancellationToken = default);
    }
}
