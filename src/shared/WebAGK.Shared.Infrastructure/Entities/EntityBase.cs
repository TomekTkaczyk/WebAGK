using System.ComponentModel.DataAnnotations;
using WebAGK.Shared.Abstractions.Entities;
using WebAGK.Shared.Abstractions.Events;

namespace WebAGK.Shared.Infrastructure.Entities;

public abstract class EntityBase : IEntityBase
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; }
    public DateTime ModifiedAt { get; protected set; }
    public Guid CreatedBy { get; protected set; }
    public Guid ModifiedBy { get; protected set; }
    public Guid ConcurrencyStamp { get; set; }
    
    [Timestamp]
    public uint Version { get; set; }
    

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    public void SetConcurrencyStamp()
    {
        ConcurrencyStamp = Guid.NewGuid();
    }

    public void SetCreateBy(Guid userId, DateTime timeStamp)
    {
        CreatedAt = timeStamp;
        CreatedBy = userId;
    }

    public void SetModifiedBy(Guid userId, DateTime timeStamp)
    {
        ModifiedAt = timeStamp;
        ModifiedBy = userId;
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
    public override int GetHashCode() => Id.GetHashCode();
    
    public override bool Equals(object obj) =>
        obj is EntityBase _entity && Id == _entity.Id;

    public static bool operator ==(EntityBase left, EntityBase right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(EntityBase left, EntityBase right) =>
        !(left == right);
}