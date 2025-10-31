using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class AppAccount : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string AccountName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public Guid AppId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<AccountOrderItem> AccountOrderItems { get; set; } = new List<AccountOrderItem>();

    public virtual App App { get; set; } = null!;
}
