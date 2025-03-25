using MediatR;

namespace WebAGK.Shared.Infrastructure.Events;

public abstract class DomainEventDispatcher(IMediator mediator) {
    
    public async Task DispatchAsync(AggregateRoot aggregate, CancellationToken cancellationToken)
    {
        var _event = aggregate.DomainEvents.ToList();
        aggregate.ClearDomainEvents();

        foreach (var _domainEvent in _event)
        {
            await mediator.Publish(_domainEvent, cancellationToken);
        }
    }
}