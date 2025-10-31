using Microsoft.EntityFrameworkCore;
using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Categories.DTOs;
using ShopAppStore.Application.Features.Categories.Queries;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.Categories
{
    public class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryQuery, List<GetAllCategoryDTO>>
    {
        private readonly AppDbContext _context;

        public GetAllCategoryQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<GetAllCategoryDTO>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.CategoryName) // Sắp xếp theo tên tăng dần
                .Select(c => new GetAllCategoryDTO
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    ParentId = c.ParentId,
                    Slug = c.Slug
                })
                .ToListAsync(cancellationToken);

            return Result<List<GetAllCategoryDTO>>.Success(categories);
        }
    }
}
