using AGK.Domain.Exceptions;

namespace AGK.Domain.ValueObjects;
public sealed record PageNumber
{
	public int Value { get; }

	public PageNumber(int value) {
		if(value < 1) {
			throw new InvalidPageNumberException();
		}
		Value = value;
	}

	public static implicit operator int(PageNumber value) => value?.Value ?? 1;
	public static implicit operator PageNumber(int value) => new(value);
}

