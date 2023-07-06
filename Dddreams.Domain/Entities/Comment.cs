using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;

namespace Dddreams.Domain.Entities;

public class Comment : BaseEntity, IAggregateRoot
{
    public Dream Parent { get; private set; } = null!;
    public Guid AuthorId { get; private set; }
    public IReadOnlyList<Like> Likes => _likes.ToList();
    public string Content { get; private set; }

    private List<Like> _likes { get; set; } = new();

    public static Comment Create(Guid authorId, Dream parent, string content)
    {
        var result = new Comment
        {
            AuthorId = authorId,
            Parent = parent,
            Content = content
        };

        return result;
    }

    public Like AddLike(DreamsAccount user)
    {
        var like = Like.Create(user, Id, LikableKind.Comment);
        _likes.Add(like);
        return like;
    }


    private Comment()
    {
    }

    public void Unlike(DreamsAccount user)
    {
        var like = _likes.Find(l => l.Author.Id == user.Id);

        if (like == null)
            return;
        
        _likes.Remove(like);
    }

    public bool Edit(string newContent)
    {
        Content = newContent;
        return true;
    }
}