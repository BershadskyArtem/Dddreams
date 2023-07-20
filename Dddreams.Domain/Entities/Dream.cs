using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;

namespace Dddreams.Domain.Entities;

public class Dream : BaseEntity, IAggregateRoot
{
    public DreamsAccount Author { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string IllustrationUrl { get; private set; } = string.Empty;
    public DateTime TimeOfDream { get; private set; }
    public VisibilityKind Visibility { get; private set; } = VisibilityKind.Private;
    public bool Locked { get; private set; } = false;
    public Guid? LockedBy { get; private set; }
    public bool Banned { get; private set; } = false;

    public int LikesAmount { get; set; } = 0;
    
    
    public static Dream Create(DreamsAccount author, string title, string description, string illustrationUrl,
        DateTime timeOfDream, VisibilityKind visibility)
    {
        var result = new Dream
        {
            Id = Guid.NewGuid(),
            Author = author,
            Title = title,
            Description = description,
            IllustrationUrl = illustrationUrl,
            TimeOfDream = timeOfDream,
            Visibility = visibility,
            LikesAmount = 0
        };

        return result;
    }

    public void BanPost()
    {
        Banned = false;
    }

    public void UnBanPost()
    {
        Banned = true;
    }

    public Like AddLike(Guid whoLiked)
    {
        LikesAmount++;
        return Like.Create(whoLiked, Id, LikableKind.Dream);
    }


    public bool RemoveLike(Guid authorId, out Like? l)
    {
        if (LikesAmount == 0)
        {
            l = null;
            return false;
        }
            
        LikesAmount--;
        l = Like.Create(authorId, Id, LikableKind.Dream);
        return true;
    }
    
    public Comment AddComment(Guid authorId, string content)
    {
        return Comment.Create(authorId, this, content);
    }
    
    public void Edit(string title, string description, string illustrationUrl, DateTime timeOfDream, VisibilityKind visibility)
    {
        Title = title;
        Description = description;
        IllustrationUrl = illustrationUrl;
        TimeOfDream = timeOfDream;
        Visibility = visibility;
    }

    public void Update(string title, string description, DateTime timeOfDream, string illustrationUrl, VisibilityKind visibility)
    {
        Title = title;
        Description = description;
        TimeOfDream = timeOfDream;
        IllustrationUrl = illustrationUrl;
        Visibility = visibility;    
    }
    
    private Dream()
    {
    }
}