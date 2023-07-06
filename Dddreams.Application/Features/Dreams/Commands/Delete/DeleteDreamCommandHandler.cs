using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Helpers;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Enums;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Delete;

public class DeleteDreamCommandHandler : IRequestHandler<DeleteDreamCommand, bool>
{

    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDreamCommandHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteDreamCommand request, CancellationToken cancellationToken)
    {
        var oldPost = await _dreamsRepository.GetByIdAsync(request.DreamId);

        if (oldPost == null)
            throw new BadRequestException("Post does not exist.");
        
        var userRole = await _userRepository.GetRole(request.WhoRequested);

        if (userRole == null)
            throw new NotFoundException("You do not exist");
        
        var ownsDream = oldPost.Author.Id == request.WhoRequested ||
                        ModerationAccessHelper.CanEditPost((DreamsRole)userRole);
        
        if(!ownsDream)
            throw new BadRequestException("You do not own this dream.");
        
        await _dreamsRepository.DeleteDreamAsync(request.DreamId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}