using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Employees.Core.Exceptions;

public abstract class EmployeeException(string message, int status) : WebAGKException(message, status) { }
