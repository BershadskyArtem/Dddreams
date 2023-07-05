using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;

namespace Dddreams.Domain.Entities;

public class Dream : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime TimeOfDream { get; set; }
    public VisibilityKind Visibility { get; set; } = VisibilityKind.Private;
    public List<Comment> Comments { get; set; } = new();
    public List<Like> Likes { get; set; } = new();
}