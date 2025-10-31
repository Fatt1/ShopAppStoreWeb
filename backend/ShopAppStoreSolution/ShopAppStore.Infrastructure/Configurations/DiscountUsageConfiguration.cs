using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class DiscountUsageConfiguration : IEntityTypeConfiguration<DiscountUsage>
    {
        public void Configure(EntityTypeBuilder<DiscountUsage> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Discount__3214EC0750A4F029");

            builder.ToTable("DiscountUsage");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.CreateAt).HasColumnType("datetime");

            builder.HasOne(d => d.Discount).WithMany(p => p.DiscountUsages)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DiscountUsage_fk2");

            builder.HasOne(d => d.Order).WithMany(p => p.DiscountUsages)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DiscountUsage_fk3");

            builder.HasOne(d => d.User).WithMany(p => p.DiscountUsages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DiscountUsage_fk1");
        }
    }
}