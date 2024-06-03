using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AGK.DataAccess.Conversions;
internal class GuidConverter :ValueConverter<Guid,string>
{
    public GuidConverter()
        : base(
            v => v.ToString(),
            v => new Guid(v)) { }
}
