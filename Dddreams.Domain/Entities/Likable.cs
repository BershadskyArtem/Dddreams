using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;

namespace Dddreams.Domain.Entities;

public class Likable : IHasKey<Guid>
{
    public Guid Id { get; set; }
    public Guid LikableId { get; set; }
    public LikableKind Kind { get; set; }
}