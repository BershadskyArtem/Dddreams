using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Like;

public class LikeCommentQueryHandler : IRequestHandler<LikeCommentQuery, bool>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public LikeCommentQueryHandler(ICommentsRepository commentsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _commentsRepository = commentsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(LikeCommentQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.WhoRequested);
        if (user == null)
            throw new NotFoundException("You do not exist");

        if (!user.CanLike)
            throw new BadRequestException("You are not allowed to like comment.");

        var comment = await _commentsRepository.GetByIdAsync(request.CommentId);

        if (comment == null)
            throw new BadRequestException("Comment that you are trying to like does not exist");

        var like = comment.AddLike(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}