
namespace AGK.Domain.ValueObjects;
public sealed record Password : ValueObject
{
	public string Value { get; init; }

	public Password(string value) {  
		ArgumentNullException.ThrowIfNull(value);
		Value = value; }

	public static Password Default => new("");

	public static implicit operator string(Password name) => name?.Value;

	public static implicit operator Password(string value) => new(value);

	public override IEnumerable<object> GetAtomicValues() {
		yield return Value;
	}
}
