using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class DurationRepository : SoftDeleteableRepository<Duration, Guid>, IDurationRepository
    {
        public DurationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
