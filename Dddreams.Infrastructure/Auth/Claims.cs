using Dddreams.Domain.Enums;

namespace Dddreams.Infrastructure.Auth;

public static class Claims
{
    public static class Accounts
    {
        public const string Id = "Account.Id";
    }
    
    
    public static class Roles
    {
        public const string Signature = nameof(Roles);
        public const string SuperAdmin = nameof(DreamsRole.SuperAdmin);
        public const string Admin = nameof(DreamsRole.Admin);
        public const string Moderator = nameof(DreamsRole.Moderator);
        public const string Paid = nameof(DreamsRole.Paid);
        public const string Basic = nameof(DreamsRole.Basic);    
    }
    
    public static class Comments
    {
        public const string CanComment = $"Can.Comment";
    }
    
    public static class Dreams
    {
        public const string CanPostADream = $"Can.Post.Dream";
    }
    
    public static class Likes
    {
        public const string CanLeaveALike = $"Can.Leave.Like";
    }
    
}