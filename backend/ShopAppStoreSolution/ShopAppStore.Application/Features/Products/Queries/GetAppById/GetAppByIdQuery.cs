using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Products.DTOs;

namespace ShopAppStore.Application.Features.Products.Queries.GetAppById
{
    public record GetAppByIdQuery(Guid Id) : IQuery<GetAppDTO>
    {
    }
}
