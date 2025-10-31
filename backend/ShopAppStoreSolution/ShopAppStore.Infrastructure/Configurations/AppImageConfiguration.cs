using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class AppImageConfiguration : IEntityTypeConfiguration<AppImage>
    {
        public void Configure(EntityTypeBuilder<AppImage> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__AppImage__3214EC07B8EC990C");

            builder.ToTable("AppImage");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.App).WithMany(p => p.AppImages)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AppImage_fk1");
        }
    }
}