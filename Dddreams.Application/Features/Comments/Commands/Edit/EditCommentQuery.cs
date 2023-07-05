using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Edit;

public class EditCommentQuery : BaseAuditableQuery<bool>
{
    public Guid CommentId { get; set; }
    public Comment NewData { get; set; }
}