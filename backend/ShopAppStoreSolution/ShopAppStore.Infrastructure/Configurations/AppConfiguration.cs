using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class AppConfiguration : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__App__8E2CF7F9A60BDA13");

            builder.ToTable("App");

            builder.HasIndex(e => e.Slug, "UQ__App__BC7B5FB69F4D0B55").IsUnique();

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("AppId")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.CreateAt).HasColumnType("datetime");
            builder.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 0)");
            builder.Property(e => e.OriginalPrice).HasColumnType("decimal(18, 0)");
            builder.Property(e => e.Slug).HasMaxLength(200);
            builder.Property(e => e.UpdateAt).HasColumnType("datetime");

            builder.HasOne(d => d.Blog).WithMany(p => p.Apps)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("App_fk12");

            builder.HasOne(d => d.Duration).WithMany(p => p.Apps)
                .HasForeignKey(d => d.DurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("App_fk7");
        }
    }
}