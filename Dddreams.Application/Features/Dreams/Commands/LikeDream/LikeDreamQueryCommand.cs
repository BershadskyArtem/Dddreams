using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.LikeDream;

public class LikeDreamQueryCommand : IRequestHandler<LikeDreamQuery, bool>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;

    public LikeDreamQueryCommand(IDreamsRepository dreamsRepository, IUserRepository userRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(LikeDreamQuery request, CancellationToken cancellationToken)
    {
        var postToLike = await _dreamsRepository.GetByIdAsync(request.DreamId);
        if (postToLike == null)
            throw new BadRequestException("Post does not exist");

        if (postToLike.Liked)
            return false;
        
        if (postToLike.Visibility == VisibilityKind.Private)
            return false;

        var areFriends = await _userRepository.AreFriends(request.WhoRequested, postToLike.AuthorId);

        if (postToLike.Visibility == VisibilityKind.AllFriends && !areFriends)
            return false;

        bool result = await _dreamsRepository.LikeDream(request.DreamId, request.WhoRequested);

        result = result && await _dreamsRepository.SaveChangesAsync();

        return result;
    }
}