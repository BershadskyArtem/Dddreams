using Dddreams.Application.Common.Auth;
using Dddreams.Application.Common.Exceptions;
using MediatR;
using ApplicationException = Dddreams.Application.Common.Exceptions.ApplicationException;

namespace Dddreams.Application.Features.User.Commands.Register;

public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, UserToken>
{
    private readonly IUserAuthService _userAuthRepository;


    public RegisterAccountCommandHandler(IUserAuthService userAuthRepository)
    {
        _userAuthRepository = userAuthRepository;
    }

    public async Task<UserToken> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var userToken = await _userAuthRepository.RegisterUserAsync(request);

        if (userToken == null)
            throw new BadRequestException("Failed to create Token");
        
        return userToken;
    }
}