using Dddreams.Application.Common.Auth;
using Dddreams.Application.Common.Exceptions;
using MediatR;
using ApplicationException = Dddreams.Application.Common.Exceptions.ApplicationException;

namespace Dddreams.Application.Features.User.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserToken>
{
    private readonly IUserAuthService _userAuthService;


    public LoginUserCommandHandler(IUserAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }

    public async Task<UserToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userAuthService.LoginUserAsync(request);
        if (result == null)
            throw new ApplicationException("Failed to login a user");
        return result;  
    }
}