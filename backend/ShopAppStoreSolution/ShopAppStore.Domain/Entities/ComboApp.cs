using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class ComboApp : IEntity<Guid>
{
    public Guid Id { get; set; }

    public Guid ComboId { get; set; }

    public Guid AppId { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Combo Combo { get; set; } = null!;
}
