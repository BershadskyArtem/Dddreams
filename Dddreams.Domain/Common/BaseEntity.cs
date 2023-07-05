namespace Dddreams.Domain.Common;

public class BaseEntity : AuditableEntity, IHasKey<Guid>
{
    public Guid Id { get; set; }
}

