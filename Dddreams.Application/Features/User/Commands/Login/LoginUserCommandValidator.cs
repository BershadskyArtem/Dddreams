using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Dddreams.Application.Features.User.Commands.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible);
        RuleFor(x => x.Password).NotEmpty();
    }
}