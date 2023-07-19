using FluentValidation;

namespace Dddreams.Application.Features.Dreams.Commands.Edit;

public class EditDreamCommandValidator : AbstractValidator<EditDreamCommand>
{
    public EditDreamCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.TimeOfDream).LessThan(DateTime.UtcNow);
    }
}