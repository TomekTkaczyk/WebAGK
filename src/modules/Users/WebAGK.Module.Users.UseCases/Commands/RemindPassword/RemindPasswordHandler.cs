using MediatR;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Services;

namespace WebAGK.Module.Users.UseCases.Commands.RemindPassword;
internal class RemindPasswordHandler(
	IUserRepository repository,
	IEmailConfirmerFactory emailConfirmerFactory) : IRequestHandler<RemindPasswordCommand>
{
	public async Task Handle(RemindPasswordCommand request, CancellationToken cancellationToken)
	{
		var user = await repository.GetByEmailAsync(request.Email, cancellationToken)
			?? throw new InvalidCredentialsException();

		var emailConfirmer = emailConfirmerFactory.GetEmailConfirmer();

		var forgotEmail = new Email
		{
			Body = emailConfirmer.GetRemindPasswordBody(user.Id, user.Email),
			Subject = "Remind your password in the WebAGK application",
			Recievers = [user.Email]
		};

		EmailsQueue.Add(forgotEmail);
	}
}
