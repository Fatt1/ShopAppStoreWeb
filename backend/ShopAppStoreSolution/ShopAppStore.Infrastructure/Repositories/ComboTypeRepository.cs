using Microsoft.EntityFrameworkCore;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class ComboTypeRepository : GenericRepository<ComboType, Guid>, IComboTypeRepository
    {
        public ComboTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ComboType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Id == id, cancellationToken);
        }
    }
}
