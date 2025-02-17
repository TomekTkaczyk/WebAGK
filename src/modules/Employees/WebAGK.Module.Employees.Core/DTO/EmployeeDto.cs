using System.ComponentModel.DataAnnotations;

namespace WebAGK.Module.Employees.Core.DTO;
public class EmployeeDto
{
	public Guid Id { get; set; }

	[Required]
	[StringLength(100, MinimumLength = 3)]
	public string Firstname { get; set; }

	[Required]
	[StringLength(100, MinimumLength = 3)]
	public string Lastname { get; set; }
}
