using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07E9EB7F87");

            builder.ToTable("OrderItem");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.RawTotalPrice).HasColumnType("decimal(18, 0)");

            builder.HasOne(d => d.App).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderItem_fk3");

            builder.HasOne(d => d.Combo).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ComboId)
                .HasConstraintName("OrderItem_fk5");

            builder.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderItem_fk1");
        }
    }
}