using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Create;

public class CreateDreamCommandHandler : IRequestHandler<CreateDreamCommand, Dream>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;


    public CreateDreamCommandHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
    }

    public async Task<Dream> Handle(CreateDreamCommand request, CancellationToken cancellationToken)
    {
        var allowedToPost = await _userRepository.AllowedToPost(request.WhoRequested);

        if (!allowedToPost)
            throw new BadRequestException("You are not allowed to post.");

        var dream = new Dream
        {
            Description = request.Description,
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            IllustrationUrl = request.IllustrationUrl,
            Title = request.Title,
            Visibility = request.Visibility,
            AuthorId = request.WhoRequested
        };
        
        await _dreamsRepository.CreateAsync(dream);
        await _dreamsRepository.SaveChangesAsync();
        
        return dream;
    }
}