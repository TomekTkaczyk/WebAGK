using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Module.Users.UseCases.Specifications;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Abstractions.Exceptions;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Services;

namespace WebAGK.Module.Users.UseCases.Commands.SignUp;
internal class SignUpHandler(
	IUserRepository repository,
	IUserUnitOfWork	unitOfWork,
	IPasswordHasher<User> passwordHasher,
	ITokenProvider tokenProvider,
	IClock clock) : IRequestHandler<SignUpCommand, Guid>
{
	public async Task<Guid> Handle(SignUpCommand request, CancellationToken cancellationToken)
	{
		var _error = new ApiError();
		var _userExist = await repository
			.Get(new UserByEmailSpecification(request.Email))
			.AnyAsync(cancellationToken);
		if(_userExist) {
			_error.AddValidationError("Email", "email_is_unavailable", "Email is unavailable.");
		}

		_userExist = await repository
			.Get(new UserByNameSpecification(request.UserName))
			.AnyAsync(cancellationToken);
		if(_userExist) {
			_error.AddValidationError("UserName", "username_is_unavailable", "User name is unavailable.");
		}

		if(_error.ValidationErrors.Any()) {
			throw new InvalidCredentialsException()
			{
				Error = _error
			};
		}

		var _password = passwordHasher.HashPassword(default, request.Password);

		var _user = User.Create(
			name:request.UserName,
			email:request.Email,
			password: _password,
			emailToConfirm:request.ConfirmEmailUrl,
			createdAt: clock.CurrentDate());

		var _token = tokenProvider.GenerateConfirmEmailToken(_user.Id, _user.Email);

		_user.EmailConfirmToken = _token;

		repository.Add(_user);
		await unitOfWork.SaveChangesAsync(cancellationToken);
		
		await CreateEmail(
			request.ConfirmEmailUrl.Replace("__token__", _token),
			_user.EmailToConfirm,
			cancellationToken);

		return _user.Id;
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
