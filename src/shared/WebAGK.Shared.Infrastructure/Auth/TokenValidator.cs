using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace WebAGK.Shared.Infrastructure.Auth;
internal class TokenValidator(IClock clock) : ITokenValidator
{
	private JwtSecurityToken _jwt;
	private bool _isValid;

	public ITokenValidator GetToken(string token)
	{
		var _handler = new JwtSecurityTokenHandler();
		_jwt = _handler.CanReadToken(token) ? _handler.ReadJwtToken(token) : null;
		_isValid = true;

		return this;
	}

	public ITokenValidator HasValidUser(Guid userId)
	{
		var _sub = _jwt.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		_isValid &= _sub != null && _sub == userId.ToString();

		return this;
	}

	public ITokenValidator IsNotExpired()
	{
		if(_jwt is null) {
			_isValid = false;
			return this;
		}
		var _exp = _jwt.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
		if(_exp != null && long.TryParse(_exp, out var _expUnixSecons)) {
			var _expirationDate = DateTimeOffset.FromUnixTimeSeconds(_expUnixSecons).UtcDateTime;
			var _now = clock.CurrentDate;
			_isValid &= _expirationDate > _now();
		}
		else {
			_isValid = false;
		}

		return this;
	}

	public bool Validate()
	{
		return _isValid;
	}
}
