using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class ComboConfiguration : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Combo__3214EC07D425F445");

            builder.ToTable("Combo");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.ComboPrice).HasColumnType("decimal(18, 0)");
            builder.Property(e => e.CreateAt).HasColumnType("datetime");
            builder.Property(e => e.Description).HasColumnType("text");
            builder.Property(e => e.UpdateAt).HasColumnType("datetime");

            builder.HasOne(d => d.ComboType).WithMany(p => p.Combos)
                .HasForeignKey(d => d.ComboTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Combo_fk2");
        }
    }
}