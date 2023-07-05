using Dddreams.Application.Interfaces.Repositories;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.UnlikeDream;

public class UnlikeDreamQueryHandler : IRequestHandler<UnlikeDreamQuery, bool>
{
    private readonly IDreamsRepository _dreamsRepository;
    private readonly IUserRepository _userRepository;


    public UnlikeDreamQueryHandler(IDreamsRepository dreamsRepository, IUserRepository userRepository)
    {
        _dreamsRepository = dreamsRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UnlikeDreamQuery request, CancellationToken cancellationToken)
    {
        var result = await _dreamsRepository.UnlikeDream(request.DreamId, request.WhoRequested);
        result = result || await _dreamsRepository.SaveChangesAsync();
        return result;
    }
}