using Dddreams.Application.Common.Exceptions;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Queries.GetAllFromPost;

public class GetAllCommentsFromPostQueryHandler : IRequestHandler<GetAllCommentsFromPostQuery, List<Comment>>
{
    private readonly ICommentsRepository _commentsRepository;
    
    public GetAllCommentsFromPostQueryHandler(ICommentsRepository commentsRepository, IDreamsRepository dreamsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task<List<Comment>> Handle(GetAllCommentsFromPostQuery request, CancellationToken cancellationToken)
    {
        return await _commentsRepository.GetAllFromPost(request.PostId);
    }
}