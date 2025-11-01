using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Shared;
using System.ComponentModel.DataAnnotations;

namespace ShopAppStore.Application.Features.Products.Queries.FilterAppPagination
{
    public record FilterAppPaginationQuery : QueyStringParameters, IQuery<PagedList<GetAllAppDTO>>
    {
        [Required]
        public List<Guid> CategoryIds { get; init; } = new();
        public decimal? PriceFrom { get; init; }
        public decimal? PriceTo { get; init; }
        public Guid? PlatFormId { get; init; }
        public Guid? DurationId { get; init; }

        public string? OrderBy { get; init; }
        [Required]
        public string SortBy { get; init; } = string.Empty;
    }

}
