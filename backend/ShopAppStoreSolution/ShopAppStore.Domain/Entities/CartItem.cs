using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class CartItem : IEntity<Guid>
{
    public Guid Id { get; set; }

    public Guid CartId { get; set; }

    public string Type { get; set; } = null!;

    public Guid? AppId { get; set; }

    public Guid? ComboId { get; set; }

    public int Quantity { get; set; }

    public virtual App? App { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Combo? Combo { get; set; }
}
