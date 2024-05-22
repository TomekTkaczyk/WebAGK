namespace AGK.Domain.ValueObjects;
public sealed record EntityId
{
	public TypeEntityId Value { get; init; }

	public EntityId(TypeEntityId value) {
		Value = value;
	}

	public static EntityId Create() => new(0);

	public static implicit operator int(EntityId entityId) => entityId.Value;

	public static implicit operator EntityId(TypeEntityId entityId) => new(entityId);
}
