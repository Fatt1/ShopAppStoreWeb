using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class Discount : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string DiscountName { get; set; } = null!;

    public string DiscountCode { get; set; } = null!;

    public string DiscountType { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int MaxUses { get; set; }

    public int MaxUsesPerUser { get; set; }

    public bool IsActive { get; set; }

    public decimal DiscountValue { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<DiscountUsage> DiscountUsages { get; set; } = new List<DiscountUsage>();
}
