using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Delete;

public class DeleteCommentQueryHandler : IRequestHandler<DeleteCommentQuery, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentsRepository _commentsRepository;
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentQueryHandler(IUserRepository userRepository, ICommentsRepository commentsRepository, IUnitOfWork unitOfWork, IDreamsRepository dreamsRepository)
    {
        _userRepository = userRepository;
        _commentsRepository = commentsRepository;
        _unitOfWork = unitOfWork;
        _dreamsRepository = dreamsRepository;
    }

    public async Task<bool> Handle(DeleteCommentQuery request, CancellationToken cancellationToken)
    {
        var requesterRole = await _userRepository.GetRole(request.WhoRequested);
        
        if (requesterRole == null)
            throw new NotFoundException("You do not exist");
        
        var comment = await _commentsRepository.GetByIdAsync(request.CommentId);

        if (comment == null)
            throw new BadRequestException("Comment that you are trying to access does not exist");

        var canDelete = comment.AuthorId == request.WhoRequested ||
                        ModerationAccessHelper.CanEditComment((DreamsRole)requesterRole); 
        
        if (!canDelete)
            throw new BadRequestException("You are not allowed to edit this post");

        var parent = await _dreamsRepository.GetByIdAsync(comment.Parent.Id);

        if (parent == null)
            throw new NotFoundException("Cannot find parent of the comment. Please report this error.");

        parent.DeleteComment(comment);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}