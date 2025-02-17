using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.SignUp;
internal sealed record SignUpCommand : IRequest<Guid>
{
	public string UserName { get; init; }

	public string Email { get; init; }

	public string Password { get; init; }

	public string ConfirmEmailUrl { get; init; }
}
