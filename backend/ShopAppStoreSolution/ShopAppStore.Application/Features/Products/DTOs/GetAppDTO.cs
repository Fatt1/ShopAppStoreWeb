namespace ShopAppStore.Application.Features.Products.DTOs
{
    public record GetAppDTO
    {
        public Guid Id { get; set; }
        public string AppName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ThumbnailUrl { get; set; } = null!;

        public List<string> ImageUrls { get; set; } = new List<string>();

        public Guid DurationId { get; set; }
        public string DurationName { get; set; } = null!;

        public List<string> CategoryNames { get; set; } = new List<string>();

        public decimal OriginalPrice { get; set; }

        public string Slug { get; set; } = null!;

        public decimal CurrentPrice { get; set; }

        public string Status { get; set; } = null!;

        public Guid? BlogId { get; set; }

        public int Stock { get; set; }

        public double TotalReviews { get; set; }

        public double AverageRating { get; set; }

        public string? Note { get; set; }

        public ICollection<GetAppAttributeDTO> Attributes { get; set; } = new List<GetAppAttributeDTO>();
    }

    public record GetAppAttributeDTO
    {
        public string AttributeName { get; set; } = null!;
        public string AttributeValue { get; set; } = null!;
    }

}
