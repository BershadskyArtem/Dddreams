using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Edit;

public class EditDreamCommandHandler : IRequestHandler<EditDreamCommand, bool>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;
    
    
    public EditDreamCommandHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(EditDreamCommand request, CancellationToken cancellationToken)
    {
        var oldPost = await _dreamsRepository.GetByIdAsync(request.NewData.Id);

        if (oldPost == null)
            throw new BadRequestException("Post does not exist.");
        
        var userRole = await _userRepository.GetRole(request.WhoRequested);

        var isLocked = oldPost.Locked;

        //Only unlock by the same or higher stuff is allowed
        if (isLocked)
        {
            var whoLocked = oldPost.LockedBy;
            var lockerRole = await _userRepository.GetRole(whoLocked);
            var allowed = ModerationAccessHelper.HigherStuffThan(userRole, lockerRole);
            
            if (!allowed) return false;
            
            await _dreamsRepository.UpdateDreamAsync(request.NewData);
            await _dreamsRepository.SaveChangesAsync();
            return true;
        }

        var ownsDream = await _dreamsRepository.UserOwnsPost(request.WhoRequested, request.NewData.Id) ||
                        ModerationAccessHelper.CanEditPost(userRole);
        
        if (!ownsDream)
            throw new BadRequestException("You do not own this dream.");
        
        return await _dreamsRepository.UpdateDreamAsync(request.NewData);;
    }
}