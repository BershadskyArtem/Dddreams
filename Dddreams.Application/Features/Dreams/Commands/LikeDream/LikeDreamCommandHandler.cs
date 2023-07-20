using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.LikeDream;

public class LikeDreamCommandHandler : IRequestHandler<LikeDreamCommand, bool>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILikesRepository _likesRepository;

    public LikeDreamCommandHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ILikesRepository likesRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _likesRepository = likesRepository;
    }

    public async Task<bool> Handle(LikeDreamCommand request, CancellationToken cancellationToken)
    {
        var postToLike = await _dreamsRepository.GetByIdAsync(request.DreamId);
        
        if (postToLike == null)
            throw new BadRequestException("Post does not exist");
        
        var whoRequested = await _userRepository.GetByIdAsync(request.WhoRequested);

        if (whoRequested == null)
            throw new BadRequestException("You do not exist");
        
        if (postToLike.Visibility == VisibilityKind.Private && !(request.WhoRequested == postToLike.Author.Id))
            return false;

        var areFriends = await _userRepository.AreFriends(request.WhoRequested, postToLike.Author.Id);

        if (postToLike.Visibility == VisibilityKind.AllFriends && !areFriends)
            return false;

        var like = postToLike.AddLike(whoRequested.Id);

        await _dreamsRepository.UpdateDreamAsync(postToLike);
        
        var success = await _likesRepository.AddAsync(like);

        if (!success)
            throw new BadRequestException("You already liked this post");
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}