using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface IDurationRepository : IGenericRepository<Duration, Guid>, ISoftDeleteableRepository<Duration, Guid>
    {
    }
}
