using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class Combo : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public string ComboName { get; set; } = null!;

    public Guid ComboTypeId { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public bool IsDeleted { get; set; }

    public decimal ComboPrice { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<ComboApp> ComboApps { get; set; } = new List<ComboApp>();

    public virtual ComboType ComboType { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
