using ShopAppStore.Domain.Interfaces;

namespace ShopAppStore.Infrastructure.Entities;

public class Review : IEntity<Guid>
{
    public Guid Id { get; set; }

    public Guid AppId { get; set; }

    public string UserId { get; set; } = null!;

    public Guid OrderId { get; set; }

    public int Rating { get; set; }

    public string ReviewStatus { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public string AdminReply { get; set; } = null!;

    public DateTime AdminReplyAt { get; set; }

    public string? AdminReplyById { get; set; }

    public string Comment { get; set; } = null!;

    public virtual AppUser? AdminReplyByNavigation { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
