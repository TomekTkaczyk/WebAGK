using AGK.Domain.ValueObjects;

namespace AGK.Domain.Entities;

public interface IBaseEntity
{
	public EntityId Id { get; }
	public Guid ConcurrencyStamp { get; }

}
