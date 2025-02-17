using MediatR;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Infrastructure.Exceptions;
using System.IdentityModel.Tokens.Jwt;

namespace WebAGK.Module.Users.UseCases.Commands.RefreshToken;
internal class RefreshTokenHandler(
	IUserRepository repository,
	ITokenProvider tokenProvider,
	IClock clock) : IRequestHandler<RefreshTokenCommand, JsonWebToken>
{
	public async Task<JsonWebToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
	{
		var handler = new JwtSecurityTokenHandler();
		var jwtToken = handler.ReadJwtToken(request.Token);
		var sub = jwtToken.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		var exp = jwtToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
		if(exp != null && long.TryParse(exp, out var expUnixSecons)) {
			var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expUnixSecons).UtcDateTime;
			var now = clock.CurrentDate();
			if(expirationDate < now) {
				throw new UnauthorisedException();
			}
		}
		else {
			throw new UnauthorisedException();
		};

		var user = await repository.GetAsync(new Guid(sub), cancellationToken)
			?? throw new UnauthorisedException();

		if(!user.IsActive) {
			throw new UserNotActiveException(user.Id);
		}

		if(!user.RefreshToken.Equals(request.Token)) {
			throw new UnauthorisedException();
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
