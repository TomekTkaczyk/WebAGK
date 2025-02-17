using MediatR;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.Logout;
internal class LogoutHandler(
	IUserRepository repository) : IRequestHandler<LogoutCommand>
{
	public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
	{
		var user = await repository.GetAsync(request.Id, cancellationToken)
		?? throw new InvalidCredentialsException();

		user.RefreshToken = null;

		await repository.UpdateAsync(user, cancellationToken);
	}
}
