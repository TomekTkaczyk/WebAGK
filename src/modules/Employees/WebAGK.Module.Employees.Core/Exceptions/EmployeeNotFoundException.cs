using Microsoft.AspNetCore.Http;

namespace WebAGK.Module.Employees.Core.Exceptions;
internal class EmployeeNotFoundException(Guid id) : EmployeeException($"Employee wth ID: {id} was not found.", StatusCodes.Status400BadRequest)
{
	public Guid Id { get; } = id;
}
