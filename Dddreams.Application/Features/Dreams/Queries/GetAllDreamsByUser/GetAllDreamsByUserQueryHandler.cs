using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Queries.GetAllDreamsByUser;

public class GetAllDreamsByUserQueryHandler : IRequestHandler<GetAllDreamsByUserQuery, List<Dream>>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;

    public GetAllDreamsByUserQueryHandler(IDreamsRepository dreamsRepository,
        IUserRepository userRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
    }

    public async Task<List<Dream>> Handle(GetAllDreamsByUserQuery request, CancellationToken cancellationToken)
    {
        var requesterRole = (await _userRepository.GetRole(request.WhoRequested));

        if (request.DreamsAuthor == request.WhoRequested || ModerationAccessHelper.CanSeePrivateDreams(requesterRole))
            return await _dreamsRepository.GetAllByUserAsync(request.DreamsAuthor);
        
        var areFriends = await _userRepository.AreFriends(request.DreamsAuthor, request.WhoRequested);

        if (!areFriends && !ModerationAccessHelper.CanSeeForFriendsDreams(requesterRole))
            return await _dreamsRepository.GetAllPublicByUserAsync(request.DreamsAuthor);
        
        var publicAndForFriends = await _dreamsRepository.GetAllPublicOrForFriendsByUserAsync(request.DreamsAuthor);
        
        return publicAndForFriends.ToList();
    }
}