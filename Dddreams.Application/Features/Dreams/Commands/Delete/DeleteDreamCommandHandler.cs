using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Delete;

public class DeleteDreamCommandHandler : IRequestHandler<DeleteDreamCommand, bool>
{

    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;


    public DeleteDreamCommandHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteDreamCommand request, CancellationToken cancellationToken)
    {
        var oldPost = await _dreamsRepository.GetByIdAsync(request.DreamId);

        if (oldPost == null)
            throw new BadRequestException("Post does not exist.");
        
        var userRole = await _userRepository.GetRole(request.WhoRequested);
        
        var ownsDream = await _dreamsRepository.UserOwnsPost(request.WhoRequested, request.DreamId) ||
                        ModerationAccessHelper.CanEditPost(userRole);
        
        if(!ownsDream)
            throw new BadRequestException("You do not own this dream.");
        
        return await _dreamsRepository.DeleteDreamAsync(request.DreamId);
    }
}