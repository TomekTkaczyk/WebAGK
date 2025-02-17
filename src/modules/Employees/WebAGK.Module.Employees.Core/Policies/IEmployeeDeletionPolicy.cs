using WebAGK.Module.Employees.Core.Entities;

namespace WebAGK.Module.Employees.Core.Policies;
internal interface IEmployeeDeletionPolicy
{
	Task<bool> CanDeleteAsync(Employee employee);
}
