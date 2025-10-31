using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class AppRepository : SoftDeleteableSlugableRepository<App, Guid>, IAppRepository
    {
        public AppRepository(AppDbContext context) : base(context)
        {
        }
    }
}
