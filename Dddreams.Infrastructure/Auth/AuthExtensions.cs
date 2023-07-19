using System.Security.Claims;

namespace Dddreams.Infrastructure.Auth;

public static class AuthExtensions
{
    public static string GetClaimValue(this IEnumerable<Claim> claims, string claimName)
    {
        return claims.Single(x => x.Type == claimName).Value;
    }
}