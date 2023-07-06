using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.UnlikeDream;

public class UnlikeDreamQueryHandler : IRequestHandler<UnlikeDreamQuery, bool>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnlikeDreamQueryHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UnlikeDreamQuery request, CancellationToken cancellationToken)
    {
        var whoRequested = await _userRepository.GetByIdAsync(request.WhoRequested);
        
        if(whoRequested == null)
            throw new BadRequestException("You do not exist...");
        
        var dream = await _dreamsRepository.GetByIdAsync(request.DreamId);

        if (dream == null)
            throw new BadRequestException("Dream does not exist");
        
        dream.Unlike(whoRequested);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return false;
    }
}