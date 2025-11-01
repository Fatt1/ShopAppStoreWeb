using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Products.DTOs;

namespace ShopAppStore.Application.Features.Products.Queries.GetRating
{
    public record GetAllRatingQuery(Guid Id) : IQuery<List<GetAllRatingDTO>>
    {

    }


}
