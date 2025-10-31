using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface IAppRepository : IGenericRepository<App, Guid>, ISoftDeleteableRepository<App, Guid>, ISlugableRepository<App, Guid>
    {
    }
}
