using Dddreams.Application.Common.Queries;
using MediatR;

namespace Dddreams.Application.Features.Dreams.Commands.Delete;

public class DeleteDreamCommand : BaseAuditableQuery<bool>
{
    public Guid DreamId { get; set; }
}