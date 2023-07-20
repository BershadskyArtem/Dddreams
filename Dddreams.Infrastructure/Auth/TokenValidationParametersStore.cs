using Microsoft.IdentityModel.Tokens;

namespace Dddreams.Infrastructure.Auth;

public class TokenValidationParametersStore
{
    public TokenValidationParametersStore(TokenValidationParameters fullValidator, TokenValidationParameters validatorForRefresh)
    {
        FullValidator = fullValidator;
        ValidatorForRefresh = validatorForRefresh;
    }

    public TokenValidationParameters FullValidator { get; private set; }
    public TokenValidationParameters ValidatorForRefresh { get; private set; }
    
}