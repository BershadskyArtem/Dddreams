using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Edit;

public class EditDreamCommandHandler : IRequestHandler<EditDreamCommand, bool>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    
    public EditDreamCommandHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(EditDreamCommand request, CancellationToken cancellationToken)
    {
        var oldPost = await _dreamsRepository.GetByIdAsync(request.NewData.Id);

        if (oldPost == null)
            throw new NotFoundException("Post does not exist.");
        
        var userRole = await _userRepository.GetRole(request.WhoRequested);

        if (userRole == null)
            throw new NotFoundException("You do not exist");
        
        
        var isLocked = oldPost.Locked;

        //Only unlock by the same or higher stuff is allowed
        if (isLocked)
        {
            var whoLocked = oldPost.LockedBy;
            var lockerRole = await _userRepository.GetRole(whoLocked);

            if (lockerRole == null)
                throw new BadRequestException("Locker id is not present in db.");
            
            var allowed = ModerationAccessHelper.HigherStuffThan((DreamsRole)userRole, (DreamsRole)lockerRole);
            
            if (!allowed) return false;
            
            await _dreamsRepository.UpdateDreamAsync(request.NewData);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        
        var dream = await _dreamsRepository.GetByIdAsync(request.DreamId);
        
        if (dream == null)
            throw new BadRequestException("Dream that you are trying to access does not exist");

        var ownsDream = dream.Author.Id == request.WhoRequested ||
                        ModerationAccessHelper.CanEditPost((DreamsRole)userRole);
        
        if (!ownsDream)
            throw new BadRequestException("You do not own this dream.");


        dream.Edit(request.Title, request.Description, request.IllustrationUrl, request.TimeOfDream, request.Visibility);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}