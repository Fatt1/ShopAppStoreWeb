using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Review__3214EC07F78B5954");

            builder.ToTable("Review");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.AdminReplyAt).HasColumnType("datetime");
            builder.Property(e => e.CreateAt).HasColumnType("datetime");

            builder.HasOne(d => d.AdminReplyByNavigation).WithMany(p => p.ReviewAdminReplyByNavigations)
                .HasForeignKey(d => d.AdminReplyById)
                .HasConstraintName("Review_fk9");

            builder.HasOne(d => d.App).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_fk1");

            builder.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_fk3");

            builder.HasOne(d => d.User).WithMany(p => p.ReviewUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_fk2");
        }
    }
}