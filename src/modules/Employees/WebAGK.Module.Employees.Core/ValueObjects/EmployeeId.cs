using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Module.Employees.Core.ValueObjects;

public sealed record EmployeeId
{
	public Guid Value { get; }

	public EmployeeId(Guid value)
	{
		if(value == Guid.Empty) {
			throw new InvalidEntityIdException(value);
		}

		Value = value;
	}

	public static implicit operator Guid(EmployeeId date) => date.Value;

	public static implicit operator EmployeeId(Guid value) => new(value);
}
