namespace ShopAppStore.Application.Features.Products.DTOs
{
    public record GetAllRatingDTO
    {
        public Guid Id { get; init; }
        public int Rating { get; init; }
        public string Comment { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public string UserName { get; init; } = string.Empty;
    }
}
