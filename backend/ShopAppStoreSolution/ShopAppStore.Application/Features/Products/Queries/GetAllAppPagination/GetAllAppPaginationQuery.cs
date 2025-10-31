using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Shared;

namespace ShopAppStore.Application.Features.Products.Queries.GetAllAppPagination
{
    public record GetAllAppPaginationQuery : QueyStringParameters, IQuery<
        PagedList<GetAllAppDTO>>
    {
    }

}
