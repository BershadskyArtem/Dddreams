using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Create;

public class CreateCommentQueryHandler : IRequestHandler<CreateCommentQuery, bool>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IUserRepository _userRepository;
    
    public CreateCommentQueryHandler(ICommentsRepository commentsRepository, IUserRepository userRepository)
    {
        _commentsRepository = commentsRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(CreateCommentQuery request, CancellationToken cancellationToken)
    {
        var canComment = await _userRepository.AllowedToComment(request.WhoRequested);

        if (!canComment)
            throw new BadRequestException("You are not allowed to post");
        
        var result = await _commentsRepository.AddComment(request.DreamId, request.CommentToCreate, request.WhoRequested);
        return result;
    }
}