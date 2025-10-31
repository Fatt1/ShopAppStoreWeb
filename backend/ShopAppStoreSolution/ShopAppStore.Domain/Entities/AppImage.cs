using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class AppImage : IEntity<Guid>
{
    public Guid Id { get; set; }

    public Guid AppId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual App App { get; set; } = null!;
}
