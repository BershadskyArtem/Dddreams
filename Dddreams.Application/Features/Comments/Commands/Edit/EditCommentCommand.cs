using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Edit;

public class EditCommentCommand : BaseAuditableQuery<bool>
{
    public Guid CommentId { get; set; }
    public string NewContent { get; set; } = string.Empty;
}