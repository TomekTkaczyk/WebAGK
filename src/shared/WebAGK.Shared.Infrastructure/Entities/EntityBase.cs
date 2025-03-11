using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Shared.Infrastructure.Entities;

public abstract class EntityBase : IEntityBase
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; }
    public DateTime ModifiedAt { get; protected set; }
    public Guid CreatedBy { get; protected set; }
    public Guid ModifiedBy { get; protected set; }
    public Guid ConcurrencyStamp { get; private set; }

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

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator == (EntityBase left, EntityBase right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator != (EntityBase left, EntityBase right)
    {
        return !(left == right);
    }

    public bool Equals(IEntityBase other) {
        throw new NotImplementedException();
    }

    public override bool Equals(object obj)
    {
        return obj is EntityBase _other && Equals(_other);
    }

    public bool Equals(EntityBase other)
    {
        return other is not null && Id.Equals(other.Id);
    }
}