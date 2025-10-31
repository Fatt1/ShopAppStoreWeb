using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories
{
    public class CategoryRepository : SlugableRepository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
