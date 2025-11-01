using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class ComboRepository : SoftDeleteableRepository<Combo, Guid>, IComboRepository
    {
        public ComboRepository(AppDbContext context) : base(context)
        {
        }
    }
}
