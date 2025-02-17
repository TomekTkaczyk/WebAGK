using WebAGK.Module.Employees.Core.Entities;

namespace WebAGK.Module.Employees.Core.Policies;

internal class EmployeeDeletionPolicy : IEmployeeDeletionPolicy
{

	public async Task<bool> CanDeleteAsync(Employee employee)
	{

		await Task.CompletedTask;

		return true;
	}
}
