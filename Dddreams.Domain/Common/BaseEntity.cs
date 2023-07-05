using System.ComponentModel.DataAnnotations.Schema;

namespace Dddreams.Domain.Common;

public abstract class BaseEntity : AuditableEntity, IHasKey<Guid>
{
    public Guid Id { get; set; }
    private readonly List<BaseEvent> _domainEvent = new();
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvent.AsReadOnly();
    public void AddDomainEvent(BaseEvent e) => _domainEvent.Add(e);
    public void RemoveDomainEvent(BaseEvent e) => _domainEvent.Remove(e);
    public void ClearDomainEvents() => _domainEvent.Clear();
    
}

