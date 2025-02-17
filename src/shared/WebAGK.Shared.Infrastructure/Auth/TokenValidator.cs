using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace WebAGK.Shared.Infrastructure.Auth;
internal class TokenValidator(IClock clock) : ITokenValidator
{
	private JwtSecurityToken _jwt;
	private bool isValid;

	public ITokenValidator GetToken(string token)
	{
		var handler = new JwtSecurityTokenHandler();
		_jwt = handler.CanReadToken(token) ? handler.ReadJwtToken(token) : null;
		isValid = true;

		return this;
	}

	public ITokenValidator HasValidUser(Guid userId)
	{
		var sub = _jwt.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		isValid &= sub != null && sub == userId.ToString();

		return this;
	}

	public ITokenValidator IsNotExpired()
	{
		if(_jwt is null) {
			isValid = false;
			return this;
		}
		var exp = _jwt.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
		if(exp != null && long.TryParse(exp, out var expUnixSecons)) {
			var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expUnixSecons).UtcDateTime;
			var now = clock.CurrentDate;
			isValid &= expirationDate > now();
		}
		else {
			isValid = false;
		}

		return this;
	}

	public bool Validate()
	{
		return isValid;
	}
}
