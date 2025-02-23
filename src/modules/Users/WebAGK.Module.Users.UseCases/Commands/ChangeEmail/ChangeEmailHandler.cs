using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Module.Users.UseCases.Specifications;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Repositories;
using WebAGK.Shared.Infrastructure.Services;

namespace WebAGK.Module.Users.UseCases.Commands.ChangeEmail;

internal class ChangeEmailHandler(
	IUserRepository repository,
	IUserUnitOfWork unitOfWork,
	ITokenProvider tokenProvider) : IRequestHandler<ChangeEmailCommand>
{
	public async Task Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
	{
		var _user = await repository.Get(new ByIdSpecification<User>(request.Id))
				.SingleOrDefaultAsync(cancellationToken)
		?? throw new InvalidCredentialsException();

		var _email = request.Email;

		var _anotherUser = await repository.Get(new UserByEmailSpecification(_email))
			.SingleOrDefaultAsync(cancellationToken);

		if(_anotherUser is not null) {
			if(!_user.Id.Equals(_anotherUser.Id)) {
				throw new EmailIsInUseException();
			}
			if(_user.Email.Equals(_email) && _user.EmailConfirm) {
				throw new EmailNotChanged();
			}
		}

		var _token = tokenProvider.GenerateConfirmEmailToken(request.Id, _email);

		_user.EmailConfirmToken = _token;
		_user.EmailToConfirm = _email;

		await CreateEmail(
			request.ConfirmEmailUrl + "?token=" + _token,
			_email,
			cancellationToken);

		repository.Update(_user);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}

	private static async Task CreateEmail(string confirmEmailUrl, string emailAddress, CancellationToken cancellationToken)
	{
		var _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmailConfirmTokenTemplate.html");
		var _template = await File.ReadAllTextAsync(_path, cancellationToken);
		var _email = new EmailMessage
		{
			Body = _template.Replace("{{ConfirmUrl}}", confirmEmailUrl),
			Subject = "Potwierdzenie adresu email w aplikacji WebAGK",
			Recievers = [emailAddress]
		};
		EmailsQueue.Add(_email);
	}
}
