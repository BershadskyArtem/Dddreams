using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Dddreams.Domain.Entities;

public class DreamsAccount : IdentityUser<Guid>, IAggregateRoot
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Nickname { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DreamsRole Role { get; private set; } = DreamsRole.Basic;
    public DateTime DateOfBirth { get; private set; }
    public IReadOnlyList<Dream> Dreams => _dreams;
    public IReadOnlyList<Comment> Comments => _comments;
    public IReadOnlyList<DreamsAccount> Friends => _friends;
    public IReadOnlyList<Like> Likes => _likes.ToList();
    

    public bool Active { get; private set; } = true;
    public bool CanPost { get; private set; } = true;
    public bool CanComment { get; private set; } = true;
    public bool CanLike { get; private set; } = true;

    public static DreamsAccount Create(string firstName, string lastName, string nickname, string email,
        string description, DateTime dateOfBirth, DreamsRole role)
    {
        var result = new DreamsAccount
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Nickname = nickname,
            UserName = nickname,
            Email = email,
            Description = description,
            DateOfBirth = dateOfBirth,
            Role = role
        };
        
        return result;
    }

    public void BanPosting()
    {
        CanPost = false;
    }

    public void UnbanPosting()
    {
        CanPost = true;
    }

    public Dream AddDream(string title, string description, string illustrationUrl, DateTime dateOfDream,
        VisibilityKind visibility)
    {
        var dream = Dream.Create(this, title, description, illustrationUrl, dateOfDream, visibility);
        _dreams.Add(dream);
        return dream;
    }


    private List<Dream> _dreams { get; set; } = new();
    private List<Comment> _comments { get; set; } = new();
    private List<DreamsAccount> _friends { get; set; } = new();

    private List<Like> _likes { get; set; } = new();
    


    private DreamsAccount()
    {
    }
}