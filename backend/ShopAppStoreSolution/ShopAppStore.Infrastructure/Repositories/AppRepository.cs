using Microsoft.EntityFrameworkCore;
using ShopAppStore.Domain.Constants;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class AppRepository : SoftDeleteableSlugableRepository<App, Guid>, IAppRepository
    {
        public AppRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Guid>> GetExistingAppIdsAsync(List<Guid> appIds, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(a => appIds.Contains(a.Id) && !a.IsDeleted)
                .Select(a => a.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<Dictionary<Guid, int>> GetAppStocksAsync(List<Guid> appIds, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(a => appIds.Contains(a.Id) && !a.IsDeleted)
                .Select(a => new
                {
                    AppId = a.Id,
                    Stock = a.AppAccounts.Count(acc => acc.Status == AppAccountStatus.Available)
                })
                .ToDictionaryAsync(x => x.AppId, x => x.Stock, cancellationToken);
        }

        public async Task<bool> AllAppsHaveStockAsync(List<Guid> appIds, CancellationToken cancellationToken = default)
        {
            var stocks = await GetAppStocksAsync(appIds, cancellationToken);

            // Kiểm tra tất cả apps đều có trong dictionary và stock > 0
            return appIds.All(id => stocks.ContainsKey(id) && stocks[id] > 0);
        }
    }
}
