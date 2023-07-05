using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;

namespace Dddreams.Application.Features.Comments.Queries.GetAllFromPostPaginated;

public class GetAllCommentsFromPostPaginatedQuery : BaseAuditableQuery<List<Comment>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Guid PostId { get; set; }
}