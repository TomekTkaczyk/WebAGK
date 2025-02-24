using MediatR;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Infrastructure.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.RefreshToken;
internal class RefreshTokenHandler(
	IUserRepository repository,
	IUserUnitOfWork unitOfWork,
	ITokenProvider tokenProvider,
	IClock clock) : IRequestHandler<RefreshTokenCommand, JsonWebToken>
{
	public async Task<JsonWebToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
	{
		var _handler = new JwtSecurityTokenHandler();
		var _jwtToken = _handler.ReadJwtToken(request.Token);
		var _sub = _jwtToken.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		var _exp = _jwtToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
		if(_exp != null && long.TryParse(_exp, out var _expUnixSecons)) {
			var _expirationDate = DateTimeOffset.FromUnixTimeSeconds(_expUnixSecons).UtcDateTime;
			var _now = clock.CurrentDate();
			if(_expirationDate < _now) {
				throw new UnauthorisedException();
			}
		}
		else {
			throw new UnauthorisedException();
		};

		if (_sub is null) {
			throw new UnauthorisedException();
		}

		var _user = await repository
			.Get(new ByIdSpecification<User>(new Guid(_sub)))
			.SingleOrDefaultAsync(cancellationToken)       
			?? throw new UnauthorisedException();

		if(!_user.ActiveStatus) {
			throw new UserNotActiveException(_user.Id);
		}

		if(!_user.RefreshToken.Equals(request.Token)) {
			throw new UnauthorisedException();
		}

		var _claims = new Dictionary<string, IEnumerable<string>>
		{
			{ "role", [_user.Role] },
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
