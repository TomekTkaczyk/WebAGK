using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObjects;

public sealed record PersonalId {
     
    private readonly string _value;
    private readonly string _countryCode;

    private PersonalId(string value, string countryCode = null) {
        _countryCode = countryCode ?? "";
        if (!IsValid(value)) {
            throw new InvalidPersonalIdException(value);
        }
        _value = value;
        _countryCode = countryCode;
    }
    
    public static implicit operator string(PersonalId data) => data?._value;

    public static implicit operator PersonalId(string value) => 
        string.IsNullOrWhiteSpace(value) ? null : new PersonalId(value);

    public static bool IsValid(string value, string countryCode = null) {
        if (value is null) {
            return true;
        }
        var _countryCode = countryCode ?? "";
        return _countryCode switch {
            "" => IsValidPolishPersonalId(value),
            "PL" => IsValidPolishPersonalId(value),
            _ => false
        };
    }

    public static PersonalId NewPersonalId(string personalId, string countryCode = null)
        => new PersonalId(personalId, countryCode);
    
    private static bool IsValidPolishPersonalId(string personalId) {
        if (personalId.Length != 11) {
            return false;
        }
        int[] _weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        var _checksum = personalId
            .Take(10)
            .Select((digit, index) => (digit - '0') * _weights[index])
            .Sum();

        var _controlDigit = (10 - (_checksum % 10)) % 10;
        
        return _controlDigit == (personalId[10] - '0');
    }
}