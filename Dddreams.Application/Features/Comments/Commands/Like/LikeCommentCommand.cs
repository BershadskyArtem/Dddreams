using Dddreams.Application.Common.Queries;

namespace Dddreams.Application.Features.Comments.Commands.Like;

public class LikeCommentCommand : BaseAuditableQuery<bool>
{
    public Guid CommentId { get; set; }
}
