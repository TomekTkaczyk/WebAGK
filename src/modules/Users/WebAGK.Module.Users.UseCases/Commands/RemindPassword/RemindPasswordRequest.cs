namespace WebAGK.Module.Users.UseCases.Commands.RemindPassword;
internal sealed record RemindPasswordRequest
{
	public string Email { get; init; }
}
