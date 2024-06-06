using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGK.Domain.ValueObjects;
public record LastName : Name
{
	public LastName(string value) : base(value) { }


	public static implicit operator string(LastName name) => name?.Value;

	public static implicit operator LastName(string value) => new(value);

}
