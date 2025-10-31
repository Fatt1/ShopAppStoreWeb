using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__CartItem__3214EC07A6FDC39B");

            builder.ToTable("CartItem");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.Type).HasMaxLength(50);

            builder.HasOne(d => d.App).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("CartItem_fk3");

            builder.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartItem_fk1");

            builder.HasOne(d => d.Combo).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ComboId)
                .HasConstraintName("CartItem_fk4");
        }
    }
}