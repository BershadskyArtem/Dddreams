using MediatR;

namespace Dddreams.Application.Common.Queries;

public class BaseAuditableQuery<T> : IRequest<T>
{
    public Guid WhoRequested { get; set; }
}