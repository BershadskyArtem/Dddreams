using Dddreams.Application.Common.Auth;
using Dddreams.Application.Common.Queries;

namespace Dddreams.Application.Features.User.Commands.Refresh;

public class RefreshTokenCommand : BaseAuditableQuery<UserToken>
{
    public string RefreshToken { get; set; }
    public string Token { get; set; }
}