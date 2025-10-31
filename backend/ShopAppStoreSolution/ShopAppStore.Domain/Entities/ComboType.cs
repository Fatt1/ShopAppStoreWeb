using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class ComboType : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int QuantityRequired { get; set; }

    public virtual ICollection<Combo> Combos { get; set; } = new List<Combo>();
}
