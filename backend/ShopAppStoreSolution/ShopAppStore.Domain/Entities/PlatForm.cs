using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class PlatForm : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string PlatFormName { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public virtual ICollection<AppPlatForm> AppPlatForms { get; set; } = new List<AppPlatForm>();
}
