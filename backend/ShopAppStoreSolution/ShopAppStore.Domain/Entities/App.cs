using ShopAppStore.Domain.Common;
using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public partial class App : IHasSlug, ISoftDelete, IEntity<Guid>
{

    public Guid Id { get; set; }
    public string AppName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ThumbnailUrl { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public bool IsDeleted { get; set; }

    public Guid DurationId { get; set; }

    public decimal OriginalPrice { get; set; }

    public string Slug { get; set; } = null!;

    public decimal CurrentPrice { get; set; }

    public string Status { get; set; } = null!;

    public Guid? BlogId { get; set; }

    public int SoldCount { get; set; }

    public virtual ICollection<AppAccount> AppAccounts { get; set; } = new List<AppAccount>();

    public virtual ICollection<AppCategory> AppCategories { get; set; } = new List<AppCategory>();

    public virtual ICollection<AppImage> AppImages { get; set; } = new List<AppImage>();

    public virtual ICollection<AppPlatForm> AppPlatForms { get; set; } = new List<AppPlatForm>();

    public virtual ICollection<AttributeApp> AttributeApps { get; set; } = new List<AttributeApp>();

    public virtual Blog? Blog { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<ComboApp> ComboApps { get; set; } = new List<ComboApp>();

    public virtual Duration Duration { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
