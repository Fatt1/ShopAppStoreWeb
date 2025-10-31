using ShopAppStore.Domain.Common;
using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class BlogCategory : IHasSlug, IEntity<Guid>
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public Guid? ParentId { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public string Slug { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<BlogCategory> Children { get; set; } = new List<BlogCategory>();

    public virtual BlogCategory? Parent { get; set; }
}
