using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.RemindPassword;
internal sealed record RemindPasswordCommand : IRequest
{
	public string Email { get; init; }

	public string ResetPasswordUrl { get; init; }
}
