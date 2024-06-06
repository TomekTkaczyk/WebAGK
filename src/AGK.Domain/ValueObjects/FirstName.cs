using AGK.Domain.Exceptions.Users;

namespace AGK.Domain.ValueObjects;
public record FirstName : Name
{
	public FirstName(string value) : base(value) { }


	public static implicit operator string(FirstName name) => name?.Value;

	public static implicit operator FirstName(string value) => new(value);

}
