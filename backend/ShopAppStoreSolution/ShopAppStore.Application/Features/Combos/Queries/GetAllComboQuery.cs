using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Combos.DTOs;

namespace ShopAppStore.Application.Features.Combos.Queries
{
    public record GetAllComboQuery : IQuery<List<GetAllComboDTO>>
    {

    }

}
