using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__BlogCate__3214EC07CA767422");

            builder.ToTable("BlogCategory");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.CategoryName).HasMaxLength(150);
            builder.Property(e => e.CreateAt).HasColumnType("datetime");
            builder.Property(e => e.Slug).HasMaxLength(200);
            builder.Property(e => e.UpdateAt).HasColumnType("datetime");

            builder.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("BlogCategory_fk2");
        }
    }
}