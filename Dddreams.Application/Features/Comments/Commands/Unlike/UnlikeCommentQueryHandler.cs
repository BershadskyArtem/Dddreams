using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Unlike;

public class UnlikeCommentQueryHandler : IRequestHandler<UnlikeCommentCommand, bool>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILikesRepository _likesRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UnlikeCommentQueryHandler(ICommentsRepository commentsRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, ILikesRepository likesRepository)
    {
        _commentsRepository = commentsRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _likesRepository = likesRepository;
    }

    public async Task<bool> Handle(UnlikeCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.WhoRequested);

        if (user == null)
            throw new NotFoundException("You do not exist");
        
        var comment = await _commentsRepository.GetByIdAsync(request.CommentId);
        
        if (comment == null)
            throw new BadRequestException("Comment does not exist");
        
        var like = comment.Unlike(user);

        if (like == null)
            return false;   

        _likesRepository.Delete(like);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}