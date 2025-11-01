namespace ShopAppStore.Application.Models
{
    public class ImageUploadResultModel
    {
        public string PublicId { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Error { get; set; } = string.Empty;
    }
}
