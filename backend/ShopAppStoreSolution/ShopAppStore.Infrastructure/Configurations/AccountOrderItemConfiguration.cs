using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class AccountOrderItemConfiguration : IEntityTypeConfiguration<AccountOrderItem>
    {
        public void Configure(EntityTypeBuilder<AccountOrderItem> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__AccountO__3214EC07F4BA3347");

            builder.ToTable("AccountOrderItem");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.ActivatedAt).HasColumnType("datetime");
            builder.Property(e => e.ExpireTime).HasColumnType("datetime");

            builder.HasOne(d => d.Account).WithMany(p => p.AccountOrderItems)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AccountOrderItem_fk7");

            builder.HasOne(d => d.OrderItem).WithMany(p => p.AccountOrderItems)
                .HasForeignKey(d => d.OrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AccountOrderItem_fk3");
        }
    }
}