namespace ShopAppStore.Application.Features.Combos.DTOs
{
    public record GetAllComboDTO
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public string ComboName { get; set; } = null!;

        public Guid ComboTypeId { get; set; }

        public decimal ComboPrice { get; set; }

        public int Stock { get; set; }
        public List<ComboAppInfoDTO> Apps { get; set; } = new List<ComboAppInfoDTO>();

    }

    public record ComboAppInfoDTO
    {
        public Guid AppId { get; set; }
        public string AppName { get; set; } = null!;
        public string ThumbnailUrl { get; set; } = null!;
        public decimal CurrentPrice { get; set; }
    }
}
