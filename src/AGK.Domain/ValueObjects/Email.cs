using AGK.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace AGK.Domain.ValueObjects;
public partial record Email	: ValueObject
{
	private const string _regex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
	
	public string Value { get; init; }

    public Email(string value)
    {
		if(string.IsNullOrEmpty(value)
			|| value.Length > 50) {
			throw new InvalidEmailException(value);
		}

		value = value.ToLowerInvariant(); 

		if(!EmailRegex().Match(value).Success)
			throw new InvalidEmailException(value);

		Value = value;
	}

	public static implicit operator string(Email email) => email?.Value;

	public static implicit operator Email(string email) => new(email);

	public override IEnumerable<object> GetAtomicValues() {
		yield return Value;
	}

	[GeneratedRegex(_regex)]
	private static partial Regex EmailRegex();

}
