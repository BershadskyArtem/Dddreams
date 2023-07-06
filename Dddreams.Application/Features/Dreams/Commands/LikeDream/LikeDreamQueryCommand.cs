using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.LikeDream;

public class LikeDreamQueryCommand : IRequestHandler<LikeDreamQuery, bool>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LikeDreamQueryCommand(IDreamsRepository dreamsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(LikeDreamQuery request, CancellationToken cancellationToken)
    {
        var postToLike = await _dreamsRepository.GetByIdAsync(request.DreamId);
        var whoRequested = await _userRepository.GetByIdAsync(request.WhoRequested);

        if (whoRequested == null)
            throw new BadRequestException("You do not exist");
        
        if (postToLike == null)
            throw new BadRequestException("Post does not exist");
        
        if (postToLike.Visibility == VisibilityKind.Private && !(request.WhoRequested == postToLike.Author.Id))
            return false;

        var areFriends = await _userRepository.AreFriends(request.WhoRequested, postToLike.Author.Id);

        if (postToLike.Visibility == VisibilityKind.AllFriends && !areFriends)
            return false;

        var like = postToLike.AddLike(whoRequested);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}