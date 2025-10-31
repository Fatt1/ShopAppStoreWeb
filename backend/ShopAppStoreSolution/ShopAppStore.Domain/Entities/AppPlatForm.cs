using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class AppPlatForm : IEntity<Guid>
{
    public Guid Id { get; set; }

    public Guid AppId { get; set; }

    public Guid PlatFormId { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual PlatForm PlatForm { get; set; } = null!;
}
