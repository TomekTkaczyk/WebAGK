using WebAGK.Shared.Abstractions.Events;
using WebAGK.Shared.Infrastructure.Messaging;

namespace WebAGK.Module.Insurers.Core.Events;

internal record InsurerCreatedEvent(Guid InsurerId) 
    : Message("InsurerCreatedEvent", "Insurer"), IDomainEvent;