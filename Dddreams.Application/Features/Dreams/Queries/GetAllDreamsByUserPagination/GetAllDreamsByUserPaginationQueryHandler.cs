using Dddreams.Application.Common.Exceptions;
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
        if (requesterRole == null)
            throw new BadRequestException("You do not exist.");

        if (request.DreamsAuthor == request.WhoRequested ||
            ModerationAccessHelper.CanSeePrivateDreams((DreamsRole)requesterRole))
        {
            var allPosts = await _dreamsRepository.GetAllByUserPaginationAsync(request.DreamsAuthor, request.PageSize,
                request.PageNumber);
            return allPosts ?? new List<Dream>();
        }
        
        var areFriends = await _userRepository.AreFriends(request.DreamsAuthor, request.WhoRequested);

        if (!areFriends && !ModerationAccessHelper.CanSeeForFriendsDreams((DreamsRole)requesterRole))
        {
            var publicPosts =  await _dreamsRepository.GetAllPublicByUserPaginationAsync(request.DreamsAuthor, request.PageSize,
                request.PageNumber);
            return publicPosts ?? new List<Dream>();
        }
        
        var result = await
            _dreamsRepository.GetAllPublicOrForFriendsByUserPaginationAsync(request.DreamsAuthor, request.PageSize,
                request.PageNumber);

        return result ?? new List<Dream>();
    }
}