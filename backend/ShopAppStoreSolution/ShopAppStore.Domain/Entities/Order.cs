using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class Order : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public string OrderStatus { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public decimal TotalApplyDiscount { get; set; }

    public Guid PaymentId { get; set; }

    public virtual ICollection<DiscountUsage> DiscountUsages { get; set; } = new List<DiscountUsage>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual AppUser User { get; set; } = null!;
}
