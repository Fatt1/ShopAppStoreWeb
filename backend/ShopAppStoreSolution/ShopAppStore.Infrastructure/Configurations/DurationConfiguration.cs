using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class DurationConfiguration : IEntityTypeConfiguration<Duration>
    {
        public void Configure(EntityTypeBuilder<Duration> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Duration__3214EC0748D654D8");

            builder.ToTable("Duration");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.CreateAt).HasColumnType("datetime");
            builder.Property(e => e.UpdateAt).HasColumnType("datetime");
        }
    }
}