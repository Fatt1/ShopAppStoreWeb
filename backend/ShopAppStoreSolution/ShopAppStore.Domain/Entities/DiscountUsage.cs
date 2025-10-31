using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class DiscountUsage : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public Guid DiscountId { get; set; }

    public Guid OrderId { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual Discount Discount { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
