using Dddreams.Application.Common.Auth;
using MediatR;

namespace Dddreams.Application.Features.User.Commands.Refresh;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, UserToken>
{
    private readonly IUserAuthService _userAuthService;

    public RefreshTokenCommandHandler(IUserAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }

    public async Task<UserToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await _userAuthService.RefreshTokenAsync(request);
        if (result == null)
            throw new ApplicationException("Unable to refresh a token");
        return result; 
    }
}