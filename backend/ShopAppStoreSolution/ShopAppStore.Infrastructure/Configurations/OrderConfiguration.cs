using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Order__3214EC076465FAC8");

            builder.ToTable("Order");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.OrderDate).HasColumnType("datetime");
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");
            builder.Property(e => e.TotalApplyDiscount).HasColumnType("decimal(18, 0)");

            builder.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_fk6");

            builder.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_fk1");
        }
    }
}