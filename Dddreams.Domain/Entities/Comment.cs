using Dddreams.Domain.Common;

namespace Dddreams.Domain.Entities;

public class Comment : BaseEntity
{
    public Dream Parent { get; set; } = null!;
    public List<Like> Likes { get; set; } = new();
}