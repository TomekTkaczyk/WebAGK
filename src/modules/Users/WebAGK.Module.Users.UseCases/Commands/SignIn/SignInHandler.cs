using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Abstractions.Auth;

namespace WebAGK.Module.Users.UseCases.Commands.SignIn;
internal class SignInHandler(
	IUserRepository repository,
	IPasswordHasher<User> passwordHasher,
	ITokenProvider tokenProvider) : IRequestHandler<SignInCommand, JsonWebToken>
{
	private const string privilegeName = "privileges";

	public async Task<JsonWebToken> Handle(SignInCommand request, CancellationToken cancellationToken)
	{
		var user = await repository.GetByIdentifierAsync(request.Identifier, cancellationToken)
		?? throw new InvalidCredentialsException();

		if(passwordHasher.VerifyHashedPassword(default, user.Password, request.Password) is not PasswordVerificationResult.Success) {
			throw new InvalidCredentialsException();
		}

		var claims = new Dictionary<string, IEnumerable<string>>
		{
			{ "role", new[] { user.Role } },
			{ "permissions", user.GetPermissions() }
		};

		var jwt = new JsonWebToken
		{
			AccessToken = tokenProvider.GenerateAccessToken(user.Id, claims),
			RefreshToken = tokenProvider.GenerateRefreshToken(user.Id),
		};

		user.RefreshToken = jwt.RefreshToken;
		await repository.UpdateAsync(user, cancellationToken);

		return jwt;
	}
}
