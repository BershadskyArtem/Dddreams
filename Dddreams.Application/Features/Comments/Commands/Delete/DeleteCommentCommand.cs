using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Delete;

public class DeleteCommentCommand : BaseAuditableQuery<bool>
{
    public Guid CommentId { get; set; }
}