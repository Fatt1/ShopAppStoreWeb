using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Category__3214EC07BF69C415");

            builder.ToTable("Category");

            builder.HasIndex(e => e.Slug, "UQ__Category__BC7B5FB6F346B578").IsUnique();

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.CreateAt).HasColumnType("datetime");
            builder.Property(e => e.Slug).HasMaxLength(200);
            builder.Property(e => e.UpdateAt).HasColumnType("datetime");

        }
    }
}