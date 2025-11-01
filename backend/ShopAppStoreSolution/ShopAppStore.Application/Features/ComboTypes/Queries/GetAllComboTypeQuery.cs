using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.ComboTypes.DTOs;

namespace ShopAppStore.Application.Features.ComboTypes.Queries
{
    public record GetAllComboTypeQuery : IQuery<List<GetAllComboTypeDTO>>
    {
    }
}
