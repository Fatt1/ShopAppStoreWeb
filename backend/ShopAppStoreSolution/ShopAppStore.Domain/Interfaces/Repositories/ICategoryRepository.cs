using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category, Guid>, ISlugableRepository<Category, Guid>
    {
    }
}
