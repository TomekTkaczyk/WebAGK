using AGK.Domain.Exceptions;

namespace AGK.Domain.ValueObjects;
public sealed record PageSize
{
	public int Value { get; }

	public PageSize(int value) {
		if(value < 0) {
			throw new InvalidPageSizeException();
		}
		Value = value;
	}

	public static implicit operator int(PageSize value) => value?.Value ?? 0;
	public static implicit operator PageSize(int value) => new(value);
}

