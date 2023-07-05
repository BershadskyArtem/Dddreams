using MediatR;

namespace Dddreams.Domain.Common;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents)
    {
        foreach (var entitiesWithEvent in entitiesWithEvents)
        {
            var events = entitiesWithEvent.DomainEvents.ToArray();
            entitiesWithEvent.ClearDomainEvents();
            foreach (var baseEvent in events)
            {
                await _mediator.Publish(baseEvent).ConfigureAwait(false);
            }
        }
    }
}