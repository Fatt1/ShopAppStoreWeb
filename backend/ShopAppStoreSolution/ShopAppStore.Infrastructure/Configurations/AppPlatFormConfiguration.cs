using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class AppPlatFormConfiguration : IEntityTypeConfiguration<AppPlatForm>
    {
        public void Configure(EntityTypeBuilder<AppPlatForm> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__AppPlatF__3214EC07A6C30CE5");

            builder.ToTable("AppPlatForm");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.App).WithMany(p => p.AppPlatForms)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AppPlatForm_fk1");

            builder.HasOne(d => d.PlatForm).WithMany(p => p.AppPlatForms)
                .HasForeignKey(d => d.PlatFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AppPlatForm_fk2");
        }
    }
}