using Dddreams.Infrastructure.Auth;
using Microsoft.AspNetCore.Http;

namespace Dddreams.Infrastructure.Extensions;

public static class HttpContextTokenExtractorExtensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        if (context.User == null)
            return Guid.Empty;
        try
        {
            var re = context.User.Claims.Single(x => x.Type == Claims.Accounts.Id).Value;
            
            return Guid.Parse(re);
        }
        catch (Exception)
        {
            return Guid.Empty;
        }
    } 
}