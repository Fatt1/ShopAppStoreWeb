using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class ComboTypeConfiguration : IEntityTypeConfiguration<ComboType>
    {
        public void Configure(EntityTypeBuilder<ComboType> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__ComboTyp__3214EC0717BC2745");

            builder.ToTable("ComboType");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}