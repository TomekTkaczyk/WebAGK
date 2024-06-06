using AGK.Domain.ValueObjects;

namespace AGK.Domain.Entities;
public abstract class BaseEntity : IBaseEntity
{
	protected BaseEntity() { }

    public EntityId Id { get; protected set; }

	public DateTime CreateTimeStamp { get; protected set; }

	public EntityId CreatedById { get; protected set; }

	public User CreatedBy { get; set; }

	public DateTime ModifyTimeStamp { get; protected set; }

	public EntityId ModifiedById { get; protected set; }

	public User ModifiedBy { get; protected set; }

	public Guid ConcurrencyStamp { get; protected set; }

	public void SetCreateOwner(TypeEntityId userId, DateTime timeStamp) {
		CreateTimeStamp = timeStamp;
		CreatedById = userId == 0 ? null : userId;
	}

	public void SetModifyOwner(TypeEntityId userId, DateTime timeStamp) {
		ModifyTimeStamp = timeStamp;
		ModifiedById = userId == 0 ? null : userId;
	}

	public void SetConcurrencyStamp() {
		ConcurrencyStamp = Guid.NewGuid();
	}

	public override int GetHashCode() {
		return Id.GetHashCode();
	}

	public static bool operator ==(BaseEntity left, BaseEntity right) {
		if(left is null && right is null) {
			return true;
		}

		return left is not null && right is not null && left.Equals(right);
	}

	public static bool operator !=(BaseEntity left, BaseEntity right) {
		return !(left == right);
	}

	public override bool Equals(object obj) {
		if(obj is null) {
			return false;
		}

		if(obj.GetType() != GetType()) {
			return false;
		}

		if(obj is not BaseEntity entity) {
			return false;
		}

		return entity.Id.ToString() == Id.ToString();
	}

	public bool Equals(BaseEntity other) {
		if(other is null) {
			return false;
		}

		if(other.GetType() != GetType()) {
			return false;
		}

		return other.Id.ToString() == Id.ToString();
	}
}
