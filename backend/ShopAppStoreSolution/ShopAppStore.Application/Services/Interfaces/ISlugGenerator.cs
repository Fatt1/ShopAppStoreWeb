namespace ShopAppStore.Application.Services.Interfaces
{
    public interface ISlugGenerator
    {
        /// <summary>
        /// Tạo slug từ text input
        /// </summary>
        /// <param name="str">Text cần convert sang slug</param>
        /// <returns>Slug đã được normalize</returns>
        string GenerateSlug(string str, bool hierarchical = true);
    }
}
