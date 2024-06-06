using AGK.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AGK.DataAccess.Conversions;
internal class UserNameConverter : ValueConverter<UserName, string>
{
	public UserNameConverter()
		: base(
			v => v.Value,
			v => new UserName(v)) { }
}
