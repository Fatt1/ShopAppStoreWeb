using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Categories.DTOs;

namespace ShopAppStore.Application.Features.Categories.Queries
{
    public record GetAllCategoryQuery : IQuery<List<GetAllCategoryDTO>>
    {
    }
}
