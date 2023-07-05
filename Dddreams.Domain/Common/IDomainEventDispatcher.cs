namespace Dddreams.Domain.Common;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents);
}