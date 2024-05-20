namespace AGK.Domain.Entities;
public abstract class BaseEntity : IBaseEntity
{
	public EntityId Id { get; private set; }

	public DateTime CreateTimeStamp { get; private set; }

	public int CreatedById { get; private set; }

	public User CreatedBy { get; private set; }

	public DateTime ModifyTimeStamp { get; private set; }

	public int ModifiedById { get; private set; }

	public User ModifiedBy { get; private set; }

	public Guid ConcurrencyStamp { get; private set; } = Guid.NewGuid();

	public void SetCreateOwner(DateTime timeStamp) {
		CreateTimeStamp = timeStamp;
		CreatedById = 1;

		SetModifyOwner(timeStamp);
	}

	public void SetModifyOwner(DateTime timeStamp) {
		ModifyTimeStamp = timeStamp;
		ModifiedById = 1;
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
