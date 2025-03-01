using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObjects;
public sealed record EntityId {
	
	private readonly Guid _value;

	public EntityId(Guid value) {
		if(value == Guid.Empty) {
			throw new InvalidEntityIdException(value);
		}

		_value = value;
	}

	public static implicit operator Guid(EntityId data) => data._value;

	public static implicit operator EntityId(Guid value) => new(value);
}

