namespace WebAGK.Shared.Infrastructure.ValueObjects;

public sealed record Address {
    public string Street { get; init; }
    public string Number { get; init; } 
    public string PostalCode { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
    
    private Address(string street, string number, string postalCode, string city, string country) {
        Street = street;
        Number = number;
        PostalCode = postalCode;
        City = city;
        Country = country;
    }

    public static Address Create(string street, string number, string postalCode, string city, string country = "Polska") {
        if(string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street address cannot be null or whitespace.", nameof(street));
        if(string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Number cannot be null or whitespace.", nameof(number));
        if(string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal code cannot be null or whitespace.", nameof(postalCode));
        if(string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be null or whitespace.", nameof(city));
        if(string.IsNullOrWhiteSpace(country)) country = "Polska";
        
        return new Address(street, number, postalCode, city, country);
    }
}
