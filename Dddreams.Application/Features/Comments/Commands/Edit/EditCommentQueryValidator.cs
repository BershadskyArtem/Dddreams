using FluentValidation;

namespace Dddreams.Application.Features.Comments.Commands.Edit;

public class EditCommentQueryValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentQueryValidator()
    {
        RuleFor(x => x.NewContent).NotEmpty().WithMessage("Content must not be empty.");
    }
}