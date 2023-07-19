using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Edit;

public class EditCommentQueryHandler : IRequestHandler<EditCommentCommand, bool>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditCommentQueryHandler(ICommentsRepository commentsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _commentsRepository = commentsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var requesterRole = await _userRepository.GetRole(request.WhoRequested);
        
        if (requesterRole == null)
            throw new NotFoundException("You do not exist.");

        var comment = await _commentsRepository.GetByIdAsync(request.CommentId);

        if (comment == null)
            throw new BadRequestException("Comment that you are trying to access does not exist.");

        
        var ownsComment = comment.AuthorId == request.WhoRequested;
        ownsComment = ownsComment || ModerationAccessHelper.CanEditComment((DreamsRole)requesterRole); 
        
        if (!ownsComment)
            throw new BadRequestException("You are not allowed to edit this comment.");

        comment.Edit(request.NewContent);

        _commentsRepository.Update(comment);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
        
    }
}