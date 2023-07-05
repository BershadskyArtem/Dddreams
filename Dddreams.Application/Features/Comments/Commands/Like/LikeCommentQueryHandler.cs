using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Like;

public class LikeCommentQueryHandler : IRequestHandler<LikeCommentQuery, bool>
{
    private readonly ICommentsRepository _commentsRepository;
    
    public LikeCommentQueryHandler(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task<bool> Handle(LikeCommentQuery request, CancellationToken cancellationToken)
    {
        var result = await _commentsRepository.LikeComment(request.CommentId,request.WhoRequested);
        return result;
    }
}