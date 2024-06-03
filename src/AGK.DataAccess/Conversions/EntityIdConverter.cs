using AGK.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AGK.DataAccess.Conversions;
internal class EntityIdConverter : ValueConverter<EntityId, TypeEntityId>
{
	public EntityIdConverter()
		: base(
			v => v.Value,
			v => new EntityId(v)) { }
}
