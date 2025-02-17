using System.ComponentModel.DataAnnotations;

namespace WebAGK.Module.Users.UseCases.Commands.SignUp;
internal sealed record SignUpRequest
{
	[Required]
	[MinLength(3), MaxLength(20)]
	public string UserName { get; init; }

	[EmailAddress]
	[Required]
	public string Email { get; init; }

	public string Password { get; init; }
}
