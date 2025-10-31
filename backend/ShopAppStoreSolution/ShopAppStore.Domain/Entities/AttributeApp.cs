using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class AttributeApp : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string AttributeName { get; set; } = null!;

    public string AttributeValue { get; set; } = null!;

    public Guid AppId { get; set; }

    public virtual App App { get; set; } = null!;
}
