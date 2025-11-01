using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface IComboTypeRepository : IGenericRepository<ComboType, Guid>
    {
        /// <summary>
        /// Lấy ComboType theo Id
        /// </summary>
        Task<ComboType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
