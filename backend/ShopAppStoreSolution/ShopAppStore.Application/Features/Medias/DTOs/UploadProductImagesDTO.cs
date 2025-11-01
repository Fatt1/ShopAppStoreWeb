namespace ShopAppStore.Application.Features.Medias.DTOs
{
    public record UploadProductImagesDTO
    {
        public string ThumbnailPublicId { get; init; } = null!;
        public string ThumbnailUrl { get; init; } = null!;
        public List<ProductImageInfo> AppImages { get; init; } = new();
    }

    public record ProductImageInfo
    {
        public string PublicId { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
