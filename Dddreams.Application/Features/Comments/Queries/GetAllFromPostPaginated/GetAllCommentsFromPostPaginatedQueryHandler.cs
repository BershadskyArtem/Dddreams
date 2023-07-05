using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Queries.GetAllFromPostPaginated;

public class GetAllCommentsFromPostPaginatedQueryHandler : IRequestHandler<GetAllCommentsFromPostPaginatedQuery, List<Comment>>
{
    private readonly ICommentsRepository _commentsRepository;

    public GetAllCommentsFromPostPaginatedQueryHandler(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task<List<Comment>> Handle(GetAllCommentsFromPostPaginatedQuery request, CancellationToken cancellationToken)
    {
        return await _commentsRepository.GetAllFromPostPagination(request.PostId, request.PageSize, request.PageNumber);

    }
}