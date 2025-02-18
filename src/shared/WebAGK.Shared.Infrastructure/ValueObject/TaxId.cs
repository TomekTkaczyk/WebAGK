using System.Text.RegularExpressions;
using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObject;

public sealed record TaxId {
    
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

        var match = Regex.Match(value, @"^(?<prefix>[A-Z]{2})?(?<nip>\d{10})$");
        if (!match.Success) {
            return false;
        }
        
        var nip = match.Groups["nip"].Value;
        var prefix = match.Groups["prefix"].Value;
        return prefix switch {
            "" => IsValidPolishTaxId(nip),
            "PL" => IsValidPolishTaxId(nip),
            _ => false
        };
    }

    private static bool IsValidPolishTaxId(string nip) {
        if (nip.Length != 10 || !nip.All(char.IsDigit))
            return false;

        int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
        var checksum = nip
            .Take(9)
            .Select((digit, index) => (digit - '0') * weights[index])
            .Sum();

        var controlDigit = checksum % 11;
        
        return controlDigit == (nip[9] - '0');
    }
}