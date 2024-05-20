
namespace AGK.Domain.ValueObjects;

public record Address : ValueObject
{
    public string Street { get; init; }
    public string Number { get; init; }
    public string Apartment { get; init; }
    public string ZipCode { get; init; }
    public string City { get; init; }
    public string Country { get; init; }

	public Address(string street, string number, string apartment, string zipCode, string city, string country) {
		Street = street ?? "";
		Number = number ?? "";
		Apartment = apartment ?? "";
		ZipCode = zipCode ?? "";
		City = city ?? "";
		Country = country ?? "";
	}

	public static Address Default => new("","","","","","");


    public override IEnumerable<object> GetAtomicValues()
    {
		yield return Street;
		yield return Number;
		yield return Apartment;
		yield return ZipCode;
		yield return City;
		yield return Country;
	}
}
