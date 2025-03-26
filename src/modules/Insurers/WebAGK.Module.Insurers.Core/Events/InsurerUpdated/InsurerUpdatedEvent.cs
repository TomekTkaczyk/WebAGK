using WebAGK.Shared.Abstractions.Events;
using WebAGK.Shared.Infrastructure.Messaging;

namespace WebAGK.Module.Insurers.Core.Events.InsurerUpdated;

internal record InsurerUpdatedEvent(Guid InsurerId, string Name) 
    : Message("InsurerUpdatedEvent", "Insurer"), IDomainEvent;