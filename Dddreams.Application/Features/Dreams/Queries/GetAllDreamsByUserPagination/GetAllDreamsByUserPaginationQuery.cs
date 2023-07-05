using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Queries.GetAllDreamsByUserPagination;

public class GetAllDreamsByUserPaginationQuery : IRequest<List<Dream>>
{
    public Guid DreamsAuthor { get; init; }
    public Guid WhoRequested { get; init; }
    public int PageSize { get; init; } = 1;
    public int PageNumber { get; init; } = 1;
    public bool ForceSee { get; init; } = false;
}