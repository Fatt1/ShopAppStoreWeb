namespace ShopAppStore.Application.Features.Products.DTOs
{
    public record GetAllAppDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string ThumbnailUrl { get; init; } = string.Empty;
        public string Slug { get; init; } = string.Empty;
        public decimal CurrentPrice { get; init; }
        public decimal OriginalPrice { get; init; }
        public int Stock { get; init; }
    }
}
