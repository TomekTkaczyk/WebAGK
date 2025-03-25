using WebAGK.Shared.Abstractions.Events;
using WebAGK.Shared.Infrastructure.Messaging;

namespace WebAGK.Module.Insurers.Core.Events;

internal record InsurerUpdatedEvent(Guid InsurerId) 
    : Message("InsurerUpdatedEvent", "Insurer"), IDomainEvent;