using FluentValidation;

namespace Dddreams.Application.Features.Dreams.Commands.Create;

public class CreateDreamCommandValidator : AbstractValidator<CreateDreamCommand>
{
    public CreateDreamCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.TimeOfDream).LessThan(DateTime.UtcNow);
    }
}