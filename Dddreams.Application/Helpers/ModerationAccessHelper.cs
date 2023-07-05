using Dddreams.Domain.Enums;

namespace Dddreams.Application.Helpers;

public static class ModerationAccessHelper
{
    private const int AdminLevel = (int)DreamsRole.Admin;
    private const int ModeratorLevel = (int)DreamsRole.Moderator;
    
    public static bool CanSeePrivateDreams(DreamsRole role) => (int)role >= AdminLevel;
    public static bool CanSeeForFriendsDreams(DreamsRole role) => (int)role >= ModeratorLevel;

    public static bool CanEditPost(DreamsRole role) => (int)role >= ModeratorLevel;
    public static bool CanEditComment(DreamsRole role) => (int)role >= ModeratorLevel;

    public static bool HigherStuffThan(DreamsRole one, DreamsRole another) => (int)one > (int)another;
}