using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Queries.GetAllDreamsByUserPagination;

public class GetAllDreamsByUserPaginationQueryHandler : IRequestHandler<GetAllDreamsByUserPaginationQuery, List<Dream>>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;

    public GetAllDreamsByUserPaginationQueryHandler(IDreamsRepository dreamsRepository,
        IUserRepository userRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
    }

    public async Task<List<Dream>> Handle(GetAllDreamsByUserPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var requesterRole = await _userRepository.GetRole(request.WhoRequested);
        if (request.DreamsAuthor == request.WhoRequested || ModerationAccessHelper.CanSeePrivateDreams(requesterRole))
            return await _dreamsRepository.GetAllByUserPaginationAsync(request.DreamsAuthor, request.PageSize,
                request.PageNumber);

        var areFriends = await _userRepository.AreFriends(request.DreamsAuthor, request.WhoRequested);
     
        if (!areFriends && !ModerationAccessHelper.CanSeeForFriendsDreams(requesterRole))
            return await _dreamsRepository.GetAllPublicByUserPaginationAsync(request.DreamsAuthor, request.PageSize,
                request.PageNumber);

        var result = await
            _dreamsRepository.GetAllPublicOrForFriendsByUserPaginationAsync(request.DreamsAuthor, request.PageSize,
                request.PageNumber);

        return result;
    }
}