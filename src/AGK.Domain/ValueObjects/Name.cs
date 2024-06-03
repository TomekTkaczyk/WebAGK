namespace AGK.Domain.ValueObjects;

public record Name : ValueObject
{
	
	public string Value { get; init; }

	public Name(string value) {
		ArgumentNullException.ThrowIfNull(value);

		Value = value;
	}

	public static Name Default => new("");


	public static implicit operator string(Name name) => name?.Value;

	public static implicit operator Name(string value) => new(value);

	public override IEnumerable<object> GetAtomicValues() {
		yield return Value;
	}
}
