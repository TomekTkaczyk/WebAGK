using System.Text.RegularExpressions;
using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.ValueObject;

public sealed partial record Email {
    
    private readonly string _value;

    private Email(string value) {
        if (string.IsNullOrWhiteSpace(value)) {
            throw new InvalidEmailException(value);
        }

        if (!EmailRegex().IsMatch(value)) {
            throw new InvalidEmailException(value);
        }
        
        _value = value;
    }
    
    public static implicit operator string(Email data) => data._value;

    public static implicit operator Email(string value) {
        if (value is null) {
            return null;
        }

        if (string.IsNullOrWhiteSpace(value)) {
            throw new InvalidEmailException(value);
        }
        
        return new Email(value);
    }
    
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled)]
    private static partial Regex EmailRegex();
}