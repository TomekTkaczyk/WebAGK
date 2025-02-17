using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.ChangePassword;
internal sealed record ChangePasswordCommand : IRequest
{
	public Guid Id { get; init; }

	public string CurrentPassword { get; init; }

	public string Password { get; init; }
}
