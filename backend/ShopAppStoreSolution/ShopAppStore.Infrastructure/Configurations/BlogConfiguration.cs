using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Blog__3214EC077C77F718");

            builder.ToTable("Blog");

            builder.HasIndex(e => e.Slug, "UQ__Blog__BC7B5FB6AD008641").IsUnique();

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.CreateAt).HasColumnType("datetime");
            builder.Property(e => e.Description).HasColumnType("text");
            builder.Property(e => e.Slug).HasMaxLength(200);
            builder.Property(e => e.Title).HasMaxLength(300);
            builder.Property(e => e.UpdateAt).HasColumnType("datetime");

            builder.HasOne(d => d.BlogCategoryNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.BlogCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Blog_fk3");
        }
    }
}