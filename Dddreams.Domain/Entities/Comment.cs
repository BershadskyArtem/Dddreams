using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;

namespace Dddreams.Domain.Entities;

public class Comment : BaseEntity, IAggregateRoot
{
    public Dream Parent { get; private set; } = null!;
    public Guid AuthorId { get; private set; }
    public DreamsAccount Author { get; private set; }
    public string Content { get; private set; }
    public Guid CommentableId { get; set; }
    public int LikesCount { get; set; }
    

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

    public Like AddLike(Guid user)
    {
        LikesCount++;
        return Like.Create(user, Id, LikableKind.Comment);
    }


    private Comment()
    {
    }

    public bool Unlike(Guid whoLiked, out Like? like)
    {
        if (LikesCount <= 0)
        {
            like = null;
            return false;
        }
        LikesCount--;
        like = Like.Create(whoLiked, Id, LikableKind.Comment);
        return true;
    }

    public bool Edit(string newContent)
    {
        Content = newContent;
        return true;
    }
}