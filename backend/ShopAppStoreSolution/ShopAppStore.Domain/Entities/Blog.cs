using ShopAppStore.Domain.Common;
using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class Blog : IHasSlug, IEntity<Guid>
{
    public Guid Id { get; set; }

    public string ThumbnailUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public Guid BlogCategoryId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public string Status { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public virtual ICollection<App> Apps { get; set; } = new List<App>();

    public virtual BlogCategory BlogCategoryNavigation { get; set; } = null!;
}
