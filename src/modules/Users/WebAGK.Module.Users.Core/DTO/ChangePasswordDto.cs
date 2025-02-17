using System.ComponentModel.DataAnnotations;

namespace WebAGK.Module.Users.Core.DTO;
public sealed record ChangePasswordDto
{
	[Required]
	public string CurrentPassword { get; set; }

	[Required]
	public string Password { get; set; }
}
