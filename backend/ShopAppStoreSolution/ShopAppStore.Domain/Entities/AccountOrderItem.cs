using System;
using System.Collections.Generic;

namespace ShopAppStore.Infrastructure.Entities;

public partial class AccountOrderItem
{
    public Guid Id { get; set; }

    public string AccountName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid OrderItemId { get; set; }

    public DateTime ExpireTime { get; set; }

    public string Status { get; set; } = null!;

    public DateTime ActivatedAt { get; set; }

    public Guid AccountId { get; set; }

    public bool IsExpired { get; set; }

    public virtual AppAccount Account { get; set; } = null!;

    public virtual OrderItem OrderItem { get; set; } = null!;
}
