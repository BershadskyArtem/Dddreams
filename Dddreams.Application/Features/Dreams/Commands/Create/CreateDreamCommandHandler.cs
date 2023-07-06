using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Create;

public class CreateDreamCommandHandler : IRequestHandler<CreateDreamCommand, Dream>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDreamCommandHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Dream> Handle(CreateDreamCommand request, CancellationToken cancellationToken)
    {
        var whoRequested = await _userRepository.GetByIdAsync(request.WhoRequested);

        if (whoRequested == null)
            throw new BadRequestException("You do not exist...");

        if (!whoRequested.CanPost)
            throw new BadRequestException("You are not allowed to post.");
        
        var createdDream = whoRequested.AddDream(request.Title, request.Description, request.IllustrationUrl, request.TimeOfDream,
            request.Visibility);
        
        await _dreamsRepository.CreateAsync(createdDream);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return createdDream;
    }
}