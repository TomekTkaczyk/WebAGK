using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObject;
public sealed record EntityId
{
	public Guid Value { get; }

	public EntityId(Guid value) {
		if(value == Guid.Empty) {
			throw new InvalidEntityIdException(value);
		}

		Value = value;
	}

	public static implicit operator Guid(EntityId date) => date.Value;

	public static implicit operator EntityId(Guid value) => new(value);
}

