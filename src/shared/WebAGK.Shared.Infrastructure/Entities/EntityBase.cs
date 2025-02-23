using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Shared.Infrastructure.Entities;
public abstract class EntityBase : IEntityBase {
	public Guid Id { get; init; } = Guid.NewGuid();
	public DateTime CreatedAt { get; protected set; }
	public DateTime ModifiedAt { get; protected set; }
	public Guid CreatedBy { get; protected set; }
	public Guid ModifiedBy { get; protected set; }
	public Guid ConcurrencyStamp { get; private set; }

	public void SetConcurrencyStamp() {
		ConcurrencyStamp = Guid.NewGuid();
	}

	public void SetCreateBy(Guid userId, DateTime timeStamp) {
		CreatedAt = timeStamp;
		CreatedBy = userId;
	}
	
	public void SetModifiedBy(Guid userId, DateTime timeStamp) {
		ModifiedAt = timeStamp;
		ModifiedBy = userId;
	}

	public override int GetHashCode() {
		return Id.GetHashCode();
	}
	
	public static bool operator ==(EntityBase left, EntityBase right) {
		if(left is null && right is null) {
			return true;
		}

		return left is not null && right is not null && left.Equals(right);
	}

	public static bool operator !=(EntityBase left, EntityBase right) {
		return !(left == right);
	}
	
	public override bool Equals(object obj) {
		if(obj is null) {
			return false;
		}

		if(obj.GetType() != GetType()) {
			return false;
		}

		if(obj is not EntityBase _entity) {
			return false;
		}

		return _entity.Id.ToString() == Id.ToString();
	}
	
	public bool Equals(EntityBase other) {
		if(other is null) {
			return false;
		}

		if(other.GetType() != GetType()) {
			return false;
		}

		return other.Id.ToString() == Id.ToString();
	}
}
