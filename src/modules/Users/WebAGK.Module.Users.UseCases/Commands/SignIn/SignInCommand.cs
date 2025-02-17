using MediatR;
using WebAGK.Shared.Abstractions.Auth;
using System.ComponentModel.DataAnnotations;

namespace WebAGK.Module.Users.UseCases.Commands.SignIn;
internal sealed record SignInCommand : IRequest<JsonWebToken>
{
	[Required]
	[MinLength(3), MaxLength(20)]
	public string Identifier { get; set; }

	public string Password { get; set; }
}
