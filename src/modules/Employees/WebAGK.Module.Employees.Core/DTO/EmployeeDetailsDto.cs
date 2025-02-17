using System.ComponentModel.DataAnnotations;

namespace WebAGK.Module.Employees.Core.DTO;
public class EmployeeDetailsDto : EmployeeDto
{
	[StringLength(1000)]
	public string Description { get; set; }
}
