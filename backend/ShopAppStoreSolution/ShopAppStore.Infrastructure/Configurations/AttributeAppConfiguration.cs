using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class AttributeAppConfiguration : IEntityTypeConfiguration<AttributeApp>
    {
        public void Configure(EntityTypeBuilder<AttributeApp> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Attribut__3214EC075B5F5BCA");

            builder.ToTable("AttributeApp");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.App).WithMany(p => p.AttributeApps)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AttributeApp_fk3");
        }
    }
}