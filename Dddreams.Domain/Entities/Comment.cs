using Dddreams.Domain.Common;

namespace Dddreams.Domain.Entities;

public class Comment : BaseEntity, IAggregateRoot
{
    public Dream Parent { get; set; } = null!;
    public List<Like> Likes { get; set; } = new();
    public Guid AuthorId { get; set; }
    public bool Liked { get; set; } = false;
}