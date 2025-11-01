namespace ShopAppStore.Application.Features.Categories.DTOs
{
    public record GetAllCategoryDTO
    {

        public Guid Id { get; set; }

        public string CategoryName { get; set; } = null!;

        public string Slug { get; set; } = null!;
    }
}
