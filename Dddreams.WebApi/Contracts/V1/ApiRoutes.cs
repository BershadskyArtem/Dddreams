namespace Dddreams.WebApi.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = $"{Root}/{Version}";

    public static class Users
    {
        public const string Register = $"{Base}/identity/register";
        public const string Login = $"{Base}/identity/login";
        public const string Refresh = $"{Base}/identity/refresh";
        //TODO: Come up with OnUserDelete strategy. Should i delete dreams with account?
        //public const string Delete = $"{Base}/identity/refresh";
    }
    
    public static class Dreams
    {
        public const string GetAllByUser = Base + "/dreams/byuser/{userId}";
        public const string GetById = Base + "/dreams/{dreamId}";
        public const string Create = $"{Base}/dreams";
        public const string Update = Base + "/dreams";
        public const string Delete = Base + "/dreams";
    }
    
    public static class Likes
    {
        public const string GiveToDream = Base + "giveliketo/dreams";
        public const string RemoveFromDream = Base + "removelike/dreams";
        
        public const string GiveToComment = Base + "giveliketo/comments";
        public const string RemoveFromComment = Base + "removelike/comments";
    }
    
    public static class Comments
    {
        public const string GetFromDream = Base + "/comments/dream/{dreamId}";
        public const string Create = Base + "/comments/dream";
        public const string Update = Base + "/comments";
        public const string Delete = Base + "/comments";
    }
    
}