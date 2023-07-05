using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Create;

public class CreateCommentQuery : BaseAuditableQuery<bool>
{
    public Comment CommentToCreate { get; set; }
    public Guid DreamId { get; set; }
}