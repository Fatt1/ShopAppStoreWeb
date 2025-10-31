using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class OrderItem : IEntity<Guid>
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public decimal RawTotalPrice { get; set; }

    public Guid AppId { get; set; }

    public string AppName { get; set; } = null!;

    public Guid? ComboId { get; set; }

    public virtual ICollection<AccountOrderItem> AccountOrderItems { get; set; } = new List<AccountOrderItem>();

    public virtual App App { get; set; } = null!;

    public virtual Combo? Combo { get; set; }

    public virtual Order Order { get; set; } = null!;
}
