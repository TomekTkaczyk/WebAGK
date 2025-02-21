using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Abstractions.Exceptions;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Services;

namespace WebAGK.Module.Users.UseCases.Commands.SignUp;
internal class SignUpHandler(
	IUserRepository repository,
	IPasswordHasher<User> passwordHasher,
	ITokenProvider tokenProvider,
	IClock clock) : IRequestHandler<SignUpCommand, Guid>
{
	public async Task<Guid> Handle(SignUpCommand request, CancellationToken cancellationToken)
	{
		var error = new ApiError();
		var user = await repository.GetByEmailAsync(request.Email, cancellationToken);
		if(user is not null) {
			error.AddValidationError("EmailMessage", "email_is_unavailable", "EmailMessage is unavailable.");
		}

		user = await repository.GetByNameAsync(request.UserName, cancellationToken);
		if(user is not null) {
			error.AddValidationError("UserName", "username_is_unavailable", "UserName is unavailable.");
		}

		if(error.ValidationErrors.Any()) {
			throw new InvalidCredentialsException()
			{
				Error = error
			};
		}

		var password = passwordHasher.HashPassword(default, request.Password);

		user = User.Create(
			name:request.UserName,
			email:request.Email,
			password: password,
			emailToConfirm:request.ConfirmEmailUrl,
			createdAt: clock.CurrentDate());

		var token = tokenProvider.GenerateConfirmEmailToken(user.Id, user.Email);

		user.EmailConfirmToken = token;

		await repository.AddAsync(user, cancellationToken);

		await CreateEmail(
			request.ConfirmEmailUrl.Replace("__token__", token),
			user.EmailToConfirm,
			cancellationToken);

		return user.Id;
	}

	private static async Task CreateEmail(string confirmEmailUrl, string emailAddress, CancellationToken cancellationToken)
	{
		var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmailConfirmTokenTemplate.html");
		var template = await File.ReadAllTextAsync(path, cancellationToken);
		var email = new EmailMessage
		{
			Body = template.Replace("{{ConfirmUrl}}", confirmEmailUrl),
			Subject = "Potwierdzenie adresu email w aplikacji WebAGK",
			Recievers = [emailAddress]
		};
		EmailsQueue.Add(email);
	}
}
