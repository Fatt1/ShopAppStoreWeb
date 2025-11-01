using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface IComboRepository : IGenericRepository<Combo, Guid>, ISoftDeleteableRepository<Combo, Guid>
    {
    }
}
