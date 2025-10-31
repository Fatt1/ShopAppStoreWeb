using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Discount__3214EC07EA9BB7BE");

            builder.ToTable("Discount");

            builder.HasIndex(e => e.DiscountCode, "UQ__Discount__A1120AF5E3188A1E").IsUnique();

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.DiscountCode).HasMaxLength(100);
            builder.Property(e => e.DiscountValue).HasColumnType("decimal(18, 0)");
            builder.Property(e => e.EndDate).HasColumnType("datetime");
            builder.Property(e => e.StartDate).HasColumnType("datetime");
        }
    }
}