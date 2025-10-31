using Microsoft.AspNetCore.Identity;

namespace ShopAppStore.Infrastructure.Entities;

public class AppUser : IdentityUser
{

    public bool IsActive { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<DiscountUsage> DiscountUsages { get; set; } = new List<DiscountUsage>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> ReviewAdminReplyByNavigations { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewUsers { get; set; } = new List<Review>();
}
