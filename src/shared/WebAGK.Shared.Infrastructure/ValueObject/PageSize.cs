using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObject;

public sealed record PageSize {
	
	private readonly int _value;

	private PageSize(int value) {
		if(value < 0) {
			throw new InvalidPageSizeException(value);
		}
		_value = value;
	}

	public static implicit operator int(PageSize data) => data?._value ?? 0;
	public static implicit operator PageSize(int value) => new(value);
}

