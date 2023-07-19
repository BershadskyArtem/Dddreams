using Dddreams.Application.Common.Queries;
using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Queries.GetAllDreamsByUser;

public class GetAllDreamsByUserQuery : BaseAuditableQuery<List<Dream>>
{
    public Guid DreamsAuthor { get; init; }
}