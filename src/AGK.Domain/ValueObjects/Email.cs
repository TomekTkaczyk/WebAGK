using AGK.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace AGK.Domain.ValueObjects;
public partial record Email	: ValueObject
{
	private const string _regex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
	
	public string Value { get; }

    public Email(string email)
    {
		ArgumentNullException.ThrowIfNull(email);

		if(!EmailRegex().Match(email).Success)
			throw new InvalidEmailException(email);

		Value = email;
	}

	public static implicit operator string(Email email) => email?.Value;

	public static implicit operator Email(string email) => new(email);

	public override IEnumerable<object> GetAtomicValues() {
		yield return Value;
	}

	[GeneratedRegex(_regex)]
	private static partial Regex EmailRegex();
}
