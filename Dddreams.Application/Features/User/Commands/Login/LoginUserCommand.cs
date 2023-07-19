using Dddreams.Application.Common.Auth;
using Dddreams.Application.Common.Queries;

namespace Dddreams.Application.Features.User.Commands.Login;

public class LoginUserCommand : BaseAuditableQuery<UserToken>
{
    public string Email { get; set; }
    public string Password { get; set; }
}