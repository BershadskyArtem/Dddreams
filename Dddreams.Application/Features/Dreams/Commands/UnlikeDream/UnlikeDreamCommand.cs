using Dddreams.Application.Common.Queries;

namespace Dddreams.Application.Features.Dreams.Commands.UnlikeDream;

public class UnlikeDreamCommand : BaseAuditableQuery<bool>
{
    public Guid DreamId { get; set; }
}