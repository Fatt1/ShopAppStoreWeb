using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class ComboAppConfiguration : IEntityTypeConfiguration<ComboApp>
    {
        public void Configure(EntityTypeBuilder<ComboApp> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__ComboApp__3214EC07F2F103B9");

            builder.ToTable("ComboApp");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.App).WithMany(p => p.ComboApps)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ComboApp_fk2");

            builder.HasOne(d => d.Combo).WithMany(p => p.ComboApps)
                .HasForeignKey(d => d.ComboId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ComboApp_fk1");
        }
    }
}