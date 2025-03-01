using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObjects;

public sealed record PageNumber {
	
	private readonly int _value;

	private PageNumber(int value) {
		if(value < 1) {
			throw new InvalidPageNumberException(value);
		}
		_value = value;
	}

	public static implicit operator int(PageNumber data) => data?._value ?? 1;
	public static implicit operator PageNumber(int value) => new(value);
}

