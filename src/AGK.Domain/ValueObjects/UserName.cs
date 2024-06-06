using AGK.Domain.Exceptions.Users;

namespace AGK.Domain.ValueObjects;
public record UserName : Name
{
	public UserName(string value) : base(value) {
		if (value.Length is > 30 or < 2) {
			throw new InvalidUserNameExcepttion();
		}
	}

	public static implicit operator string(UserName name) => name?.Value;

	public static implicit operator UserName(string value) => new(value);

	public override string ToString() => Value;
}
