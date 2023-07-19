using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.UnlikeDream;

public class UnlikeDreamCommandHandler : IRequestHandler<UnlikeDreamCommand, bool>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILikesRepository _likesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnlikeDreamCommandHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ILikesRepository likesRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _likesRepository = likesRepository;
    }

    public async Task<bool> Handle(UnlikeDreamCommand request, CancellationToken cancellationToken)
    {
        var whoRequested = await _userRepository.GetByIdAsync(request.WhoRequested);
        
        if(whoRequested == null)
            throw new BadRequestException("You do not exist...");
        
        var dream = await _dreamsRepository.GetByIdAsync(request.DreamId);

        if (dream == null)
            throw new BadRequestException("Dream does not exist");
        
        var like = dream.Unlike(whoRequested);

        if (like == null)
            return false;   
        
        _likesRepository.Delete(like);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return false;
    }
}