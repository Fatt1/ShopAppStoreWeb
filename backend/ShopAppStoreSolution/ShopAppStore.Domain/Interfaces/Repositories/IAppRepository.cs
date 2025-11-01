using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface IAppRepository : IGenericRepository<App, Guid>, ISoftDeleteableRepository<App, Guid>, ISlugableRepository<App, Guid>
    {
        /// <summary>
        /// Kiểm tra danh sách AppIds có tồn tại và chưa bị xóa không
        /// </summary>
        Task<List<Guid>> GetExistingAppIdsAsync(List<Guid> appIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lấy số lượng tồn kho của các apps (AppAccount có status Available)
        /// </summary>
        Task<Dictionary<Guid, int>> GetAppStocksAsync(List<Guid> appIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kiểm tra tất cả apps có tồn kho không (stock > 0)
        /// </summary>
        Task<bool> AllAppsHaveStockAsync(List<Guid> appIds, CancellationToken cancellationToken = default);
    }
}
