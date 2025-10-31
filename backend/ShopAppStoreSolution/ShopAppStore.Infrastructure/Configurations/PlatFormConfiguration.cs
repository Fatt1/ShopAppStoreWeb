using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class PlatFormConfiguration : IEntityTypeConfiguration<PlatForm>
    {
        public void Configure(EntityTypeBuilder<PlatForm> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__PlatForm__3214EC07B13B0BC5");

            builder.ToTable("PlatForm");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.CreateAt).HasColumnType("datetime");
            builder.Property(e => e.UpdateAt).HasColumnType("datetime");
        }
    }
}