using System.ComponentModel.DataAnnotations;

namespace WebAGK.Module.Users.Core.DTO;
public class SignUpDto
{
	[Required]
	[MinLength(3), MaxLength(20)]
	public string UserName { get; set; }

	[EmailAddress]
	[Required]
	public string Email { get; set; }

	public string Password { get; set; }
}
