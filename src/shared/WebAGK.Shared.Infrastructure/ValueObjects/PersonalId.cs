using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObjects;

public sealed record PersonalId {
     
    private readonly string _value;

    private PersonalId(string value) {
        if (!IsValid(value)) {
            throw new InvalidPersonalIdException(value);
        }
        _value = value;
    }
    
    public static implicit operator string(PersonalId data) => data?._value;

    public static implicit operator PersonalId(string value) => 
        string.IsNullOrWhiteSpace(value) ? null : new PersonalId(value);

    private static bool IsValid(string value) {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 11 || !value.All(char.IsDigit)) {
            return false;
        }

        return IsValidPolishPersonalId(value);
    }

    private static bool IsValidPolishPersonalId(string pesel) {
        int[] _weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        var _checksum = pesel
            .Take(10)
            .Select((digit, index) => (digit - '0') * _weights[index])
            .Sum();

        var _controlDigit = (10 - (_checksum % 10)) % 10;
        
        return _controlDigit == (pesel[10] - '0');
    }
}