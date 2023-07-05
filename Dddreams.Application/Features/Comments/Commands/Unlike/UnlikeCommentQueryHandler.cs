using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Unlike;

public class UnlikeCommentQueryHandler : IRequestHandler<UnlikeCommentQuery, bool>
{
    private readonly ICommentsRepository _commentsRepository;
    
    public UnlikeCommentQueryHandler(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task<bool> Handle(UnlikeCommentQuery request, CancellationToken cancellationToken)
    {
        var result = await _commentsRepository.UnlikeComment(request.CommentId, request.WhoRequested);
        result = result || await _commentsRepository.SaveChangesAsync();
        return result;
    }
}