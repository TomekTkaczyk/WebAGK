using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Module.Users.UseCases.Specifications;
using WebAGK.Shared.Abstractions.Auth;

namespace WebAGK.Module.Users.UseCases.Commands.SignIn;
internal class SignInHandler(
	IUserRepository repository,
	IUserUnitOfWork unitOfWork,
	IPasswordHasher<User> passwordHasher,
	ITokenProvider tokenProvider) : IRequestHandler<SignInCommand, JsonWebToken>
{
	private const string PrivilegeName = "privileges";

	public async Task<JsonWebToken> Handle(SignInCommand request, CancellationToken cancellationToken)
	{
		var _user = await repository
			.Get(new UserByIdentifierSpecification(request.Identifier))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new InvalidCredentialsException();

		if(passwordHasher.VerifyHashedPassword(default, _user.Password, request.Password) is not PasswordVerificationResult.Success) {
			throw new InvalidCredentialsException();
		}

		var _claims = new Dictionary<string, IEnumerable<string>>
		{
			{ "role", new[] { _user.Role } },
			{ "permissions", _user.GetPermissions() }
		};

		var _jwt = new JsonWebToken
		{
			AccessToken = tokenProvider.GenerateAccessToken(_user.Id, _claims),
			RefreshToken = tokenProvider.GenerateRefreshToken(_user.Id),
		};

		_user.RefreshToken = _jwt.RefreshToken;
		repository.Update(_user);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return _jwt;
	}
}
