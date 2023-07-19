using Dddreams.Application.Common.Queries;

namespace Dddreams.Application.Features.Comments.Commands.Unlike;

public class UnlikeCommentCommand : BaseAuditableQuery<bool>
{
    public Guid CommentId { get; set; }
}