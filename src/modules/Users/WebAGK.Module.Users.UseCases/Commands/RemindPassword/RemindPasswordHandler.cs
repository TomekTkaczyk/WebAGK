using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Module.Users.UseCases.Specifications;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Services;

namespace WebAGK.Module.Users.UseCases.Commands.RemindPassword;
internal class RemindPasswordHandler(
	IUserRepository repository,
	IEmailConfirmerFactory emailConfirmerFactory) : IRequestHandler<RemindPasswordCommand>
{
	public async Task Handle(RemindPasswordCommand request, CancellationToken cancellationToken)
	{
		var _user = await repository
			.Get(new UserByEmailSpecification(request.Email))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new InvalidCredentialsException();

		var _emailConfirmer = emailConfirmerFactory.GetEmailConfirmer();

		var _forgotEmail = new EmailMessage
		{
			Body = _emailConfirmer.GetRemindPasswordBody(_user.Id, _user.Email),
			Subject = "Remind your password in the WebAGK application",
			Recievers = [_user.Email]
		};

		EmailsQueue.Add(_forgotEmail);
	}
}
