namespace ShopAppStore.Application.Features.Products.DTOs
{
    public class CreateAppAccountDTO
    {
        public string AccountName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
