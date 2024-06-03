using AGK.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AGK.DataAccess.Conversions;
internal class NameConverter : ValueConverter<Name, string> {
    public NameConverter()
        : base(
            v => v.Value,
            v => new Name(v)) { }
}

