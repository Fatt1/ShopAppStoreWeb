using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class AppCategoryConfiguration : IEntityTypeConfiguration<AppCategory>
    {
        public void Configure(EntityTypeBuilder<AppCategory> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__AppCateg__3214EC07B311F2C7");

            builder.ToTable("AppCategory");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.App).WithMany(p => p.AppCategories)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AppCategory_fk1");

            builder.HasOne(d => d.Category).WithMany(p => p.AppCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AppCategory_fk2");
        }
    }
}