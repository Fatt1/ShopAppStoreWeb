using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class Duration : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string DurationName { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<App> Apps { get; set; } = new List<App>();
}
