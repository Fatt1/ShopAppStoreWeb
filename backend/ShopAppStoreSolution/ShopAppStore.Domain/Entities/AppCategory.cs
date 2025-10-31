using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class AppCategory : IEntity<Guid>
{
    public Guid Id { get; set; }

    public Guid AppId { get; set; }

    public Guid CategoryId { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}
