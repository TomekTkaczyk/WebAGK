using Microsoft.AspNetCore.Http;

namespace WebAGK.Module.Employees.Core.Exceptions;

internal class CannotDeleteEmployeeException(Guid id) : EmployeeException($"Employee wth ID: {id} cannot by deleted.", StatusCodes.Status400BadRequest)
{
	public Guid Id { get; } = id;
}
