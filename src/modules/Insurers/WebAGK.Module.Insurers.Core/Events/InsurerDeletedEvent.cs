using WebAGK.Shared.Abstractions.Events;
using WebAGK.Shared.Infrastructure.Messaging;

namespace WebAGK.Module.Insurers.Core.Events;

internal record InsurerDeletedEvent(Guid InsurerId) 
    : Message("InsurerDeletedEvent", "Insurer"), IDomainEvent;