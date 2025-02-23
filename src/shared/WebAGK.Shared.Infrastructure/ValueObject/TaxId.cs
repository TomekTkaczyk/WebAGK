using System.Text.RegularExpressions;
using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObject;

public sealed partial record TaxId {
    
    private readonly string _value;

    private TaxId(string value) {
        if (!IsValid(value)) {
            throw new InvalidTaxIdException(value);
        }
        _value = value;
    }
    
    public static implicit operator string(TaxId data) => data._value;

    public static implicit operator TaxId(string value) => new(value);

    private static bool IsValid(string value) {
        if (string.IsNullOrWhiteSpace(value)) {
            return false;
        }

        var _match = TaxIdRegex().Match(value);
        if (!_match.Success) {
            return false;
        }
        
        var _nip = _match.Groups["nip"].Value;
        var _prefix = _match.Groups["prefix"].Value;
        return _prefix switch {
            "" => IsValidPolishTaxId(_nip),
            "PL" => IsValidPolishTaxId(_nip),
            _ => false
        };
    }

    private static bool IsValidPolishTaxId(string nip) {
        if (nip.Length != 10 || !nip.All(char.IsDigit))
            return false;

        int[] _weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
        var _checksum = nip
            .Take(9)
            .Select((digit, index) => (digit - '0') * _weights[index])
            .Sum();

        var _controlDigit = _checksum % 11;
        
        return _controlDigit == (nip[9] - '0');
    }

    [GeneratedRegex(@"^(?<prefix>[A-Z]{2})?(?<nip>\d{10})$")]
    private static partial Regex TaxIdRegex();
}