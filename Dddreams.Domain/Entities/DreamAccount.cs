using Dddreams.Domain.Common;

namespace Dddreams.Domain.Entities;

public class DreamsAccount : BaseEntity
{
    public List<Dream> Dreams { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<DreamsAccount> Friends { get; set; } = new();
    public List<Comment> LikedComments { get; set; } = new();
    public List<Dream> LikedDreams { get; set; } = new();
    public bool Active { get; set; } = true;
}