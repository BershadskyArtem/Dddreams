using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Comments.Queries.GetAllFromPost;

public class GetAllCommentsFromPostQuery : BaseAuditableQuery<List<Comment>>
{
    public Guid PostId { get; set; }
}