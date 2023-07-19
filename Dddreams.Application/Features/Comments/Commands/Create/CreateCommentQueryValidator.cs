using FluentValidation;

namespace Dddreams.Application.Features.Comments.Commands.Create;

public class CreateCommentQueryValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentQueryValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Comment cannot be empty.");
        RuleFor(x => x.DreamId).NotEmpty();
        RuleFor(x => x.WhoRequested).NotEmpty();
    }
}