using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopAppStore.Infrastructure.Entities;

namespace ShopAppStore.Infrastructure.Configurations
{
    public class AppAccountConfiguration : IEntityTypeConfiguration<AppAccount>
    {
        public void Configure(EntityTypeBuilder<AppAccount> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__AppAccou__3214EC07893BE462");

            builder.ToTable("AppAccount");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.App).WithMany(p => p.AppAccounts)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AppAccount_fk5");
        }
    }
}