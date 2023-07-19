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
    public IReadOnlyList<Comment> Comments => _comments.ToList();
    public IReadOnlyList<Like> Likes => _likes.ToList();
    public bool Locked { get; private set; } = false;
    public Guid? LockedBy { get; private set; }
    public bool Banned { get; private set; } = false;
    
    private List<Comment> _comments { get; set; } = new();
    private List<Like> _likes { get; set; } = new();
    

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
            Visibility = visibility
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

    public Comment AddComment(Guid authorId, string content)
    {
        var comment = Comment.Create(authorId, this, content);
        _comments.Add(comment);
        return comment;
    }

    public Like AddLike(DreamsAccount whoLiked)
    {
        var like = Like.Create(whoLiked, this.Id, LikableKind.Dream);
        _likes.Add(like);
        return like;
    }

    public Like? Unlike(DreamsAccount whoRequested)
    {
        var like = _likes.Find(l => l.Author.Id == whoRequested.Id);
        return like;
    }


    private Dream()
    {
    }


    public void DeleteComment(Comment comment)
    {
        var exist = _comments.Find(c => c.Id == comment.Id);
        if (exist == null)
            return;
        _comments.Remove(exist);
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
}