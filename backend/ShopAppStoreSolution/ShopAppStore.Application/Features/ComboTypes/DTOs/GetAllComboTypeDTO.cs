namespace ShopAppStore.Application.Features.ComboTypes.DTOs
{
    public class GetAllComboTypeDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int QuantityRequired { get; set; }
    }
}
