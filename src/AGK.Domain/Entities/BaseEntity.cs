using AGK.Domain.ValueObjects;

namespace AGK.Domain.Entities;
public abstract class BaseEntity : IBaseEntity
{
    protected BaseEntity()
    {
		ConcurrencyStamp = new Guid();
	}

    public EntityId Id { get; private set; }

	public DateTime CreateTimeStamp { get; private set; }

	public EntityId CreatedById { get; private set; }

	public User CreatedBy { get; private set; }

	public DateTime ModifyTimeStamp { get; private set; }

	public EntityId ModifiedById { get; private set; }

	public User ModifiedBy { get; private set; }

	public Guid ConcurrencyStamp { get; private set; }

	public void SetCreateOwner(User user, DateTime timeStamp) {
		CreateTimeStamp = timeStamp;
		CreatedBy = user;

		SetModifyOwner(user, timeStamp);
	}

	public void SetModifyOwner(User user, DateTime timeStamp) {
		ModifyTimeStamp = timeStamp;
		ModifiedBy = user;
	}

	public void SetConcurrencyStamp() {
		ConcurrencyStamp = new Guid();
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
