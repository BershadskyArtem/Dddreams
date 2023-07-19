using Dddreams.Application.Features.User.Commands.Login;
using Dddreams.Application.Features.User.Commands.Refresh;
using Dddreams.Application.Features.User.Commands.Register;

namespace Dddreams.Application.Common.Auth;

public interface IUserAuthService
{
    Task<UserToken> RegisterUserAsync(RegisterAccountCommand request);

    Task<UserToken> LoginUserAsync(LoginUserCommand request);
    Task<UserToken> RefreshTokenAsync(RefreshTokenCommand request);
}