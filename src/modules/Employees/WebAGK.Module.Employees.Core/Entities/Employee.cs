using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Module.Employees.Core.Entities;

public class Employee : EntityBase
{
	public string Firstname { get; set; }

	public string Lastname { get; set; }

	public string Description { get; set; }
}
