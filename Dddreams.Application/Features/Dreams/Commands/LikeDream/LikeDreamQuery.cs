using Dddreams.Application.Common.Queries;

namespace Dddreams.Application.Features.Dreams.Commands.LikeDream;

public class LikeDreamQuery : BaseAuditableQuery<bool>
{
    public Guid DreamId { get; set; }
}