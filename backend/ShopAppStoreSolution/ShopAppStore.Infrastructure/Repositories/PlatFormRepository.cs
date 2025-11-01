using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class PlatFormRepository : GenericRepository<PlatForm, Guid>, IPlatFormRepository
    {
        public PlatFormRepository(AppDbContext context) : base(context)
        {
        }
    }
}
