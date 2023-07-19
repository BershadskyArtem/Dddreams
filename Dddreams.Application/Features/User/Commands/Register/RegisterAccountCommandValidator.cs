using System.Text.RegularExpressions;
using System.Xml.Schema;
using FluentValidation;

namespace Dddreams.Application.Features.User.Commands.Register;

public class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty();
        RuleFor(x => x.Nickname).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
    }
}