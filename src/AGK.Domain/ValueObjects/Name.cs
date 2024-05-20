namespace AGK.Domain.ValueObjects;

public record Name : ValueObject
{
    public string Value { get; }

    public Name(string lastname)
    {
        Value = lastname ?? "";
    }

    public static Name Default => new("");


	public static implicit operator string(Name lastname) => lastname.Value;

	public static implicit operator Name(string lastname) => new(lastname);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}