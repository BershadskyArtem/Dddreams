using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Commands.Create;

public class CreateCommentQueryHandler : IRequestHandler<CreateCommentQuery, bool>
{
    
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDreamsRepository _dreamsRepository;
    
    public CreateCommentQueryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IDreamsRepository dreamsRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _dreamsRepository = dreamsRepository;
    }

    public async Task<bool> Handle(CreateCommentQuery request, CancellationToken cancellationToken)
    {
        var whoRequested = await _userRepository.GetByIdAsync(request.WhoRequested);

        if (whoRequested == null)
            throw new NotFoundException("You do not exist");
        
        if (!whoRequested.CanComment)
            throw new BadRequestException("You are not allowed to comment.");


        var dream = await _dreamsRepository.GetByIdAsync(request.DreamId);
        
        if (dream == null)
            throw new BadRequestException("Dream does not exist");

        dream.AddComment(request.WhoRequested, request.Content);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}