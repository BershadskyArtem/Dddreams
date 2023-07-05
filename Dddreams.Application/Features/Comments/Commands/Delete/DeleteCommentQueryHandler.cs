using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Delete;

public class DeleteCommentQueryHandler : IRequestHandler<DeleteCommentQuery, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentsRepository _commentsRepository;


    public DeleteCommentQueryHandler(IUserRepository userRepository, ICommentsRepository commentsRepository)
    {
        _userRepository = userRepository;
        _commentsRepository = commentsRepository;
    }

    public async Task<bool> Handle(DeleteCommentQuery request, CancellationToken cancellationToken)
    {
        var requesterRole = await _userRepository.GetRole(request.WhoRequested);
        var allowedToEdit = await _commentsRepository.OwnsComment(request.CommentId, request.WhoRequested) ||
                            ModerationAccessHelper.CanEditComment(requesterRole);

        if (!allowedToEdit)
            throw new BadRequestException("You are not allowed to edit this post");

        var result = await _commentsRepository.DeletComment(request.CommentId);
        result = result && await _commentsRepository.SaveChangesAsync();
        return result;
    }
}