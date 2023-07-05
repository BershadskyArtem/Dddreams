using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Edit;

public class EditCommentQueryHandler : IRequestHandler<EditCommentQuery, bool>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IUserRepository _userRepository;


    public EditCommentQueryHandler(ICommentsRepository commentsRepository, IUserRepository userRepository)
    {
        _commentsRepository = commentsRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(EditCommentQuery request, CancellationToken cancellationToken)
    {
        var requesterRole = await _userRepository.GetRole(request.WhoRequested);
        var ownsComment = await _commentsRepository.OwnsComment(request.CommentId,request.WhoRequested) ||
                          ModerationAccessHelper.CanEditComment(requesterRole);

        if (!ownsComment)
            throw new BadRequestException("You are not allowed to edit this comment.");
        
        var result = await _commentsRepository.EditComment(request.CommentId, request.NewData);
        result = result && await _commentsRepository.SaveChangesAsync();
        return result;
        
    }
}