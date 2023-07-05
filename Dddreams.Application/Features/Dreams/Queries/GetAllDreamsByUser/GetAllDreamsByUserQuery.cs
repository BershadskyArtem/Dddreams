using Dddreams.Domain.Entities;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Queries.GetAllDreamsByUser;

public class GetAllDreamsByUserQuery : IRequest<List<Dream>>
{
    public Guid DreamsAuthor { get; init; }
    public Guid WhoRequested { get; init; }
}