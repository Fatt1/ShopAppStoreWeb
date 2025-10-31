using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Repositories;

public partial class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountOrderItem> AccountOrderItems { get; set; }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<AppAccount> AppAccounts { get; set; }

    public virtual DbSet<AppCategory> AppCategories { get; set; }

    public virtual DbSet<AppImage> AppImages { get; set; }

    public virtual DbSet<AppPlatForm> AppPlatForms { get; set; }

    public virtual DbSet<AttributeApp> AttributeApps { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogCategory> BlogCategories { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<ComboApp> ComboApps { get; set; }

    public virtual DbSet<ComboType> ComboTypes { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountUsage> DiscountUsages { get; set; }

    public virtual DbSet<Duration> Durations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PlatForm> PlatForms { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<AppUser> AppUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }

}
