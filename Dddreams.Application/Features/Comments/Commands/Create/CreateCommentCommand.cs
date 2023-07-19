using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Create;

public class CreateCommentCommand : BaseAuditableQuery<bool>
{
    public string Content { get; set; }
    public Guid DreamId { get; set; }
}