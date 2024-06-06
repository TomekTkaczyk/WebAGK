using AGK.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AGK.DataAccess.Conversions;
internal class RoleConverter : ValueConverter<Role, string>
{
	public RoleConverter()
		: base(
			v => v.ToString(),
			v => (Role)Enum.Parse(typeof(Role),v)) { }
}
