using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using System.Security.Claims;
using System.Text;

namespace WebAGK.Shared.Infrastructure.Auth;

internal sealed class TokenProvider : ITokenProvider
{
	private readonly SymmetricSecurityKey _securityKey;
	private readonly SigningCredentials _credentials;
	private readonly AuthOptions _options;
	private readonly IClock _clock;

	public TokenProvider(AuthOptions options, IClock clock)
	{
		_options = options;
		_clock = clock;
		_securityKey = new(Encoding.UTF8.GetBytes(_options.IssuerSigningKey));
		_credentials = new(_securityKey, SecurityAlgorithms.HmacSha256);
	}

	public string GenerateAccessToken(Guid userId, IDictionary<string, IEnumerable<string>> claims)
	{
		var _now = _clock.CurrentDate();
		var _formattedClaims = new Dictionary<string, object>();

		if(claims is not null) {
			foreach(var _claim in claims) {
				if(_claim.Value is IEnumerable<string> _enumerable) {
					_formattedClaims[_claim.Key] = _enumerable.Count() == 1 ? _enumerable.First() : _enumerable.ToArray();
				}
				else {
					_formattedClaims[_claim.Key] = _claim.Value;
				}
			}
		}

		var _tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
			[
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(_now).ToUnixTimeSeconds().ToString()),
			]),
			Issuer = "WebAGK",
			Expires = _now.Add(_options.Expiry),
			SigningCredentials = _credentials,
			Claims = _formattedClaims,
		};

		var _handler = new JsonWebTokenHandler();
		var _token = _handler.CreateToken(_tokenDescriptor);

		return _token;
	}

	public string GenerateConfirmEmailToken(Guid userId, string email)
	{
		var _now = _clock.CurrentDate();

		var _tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
			[
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(_now).ToUnixTimeSeconds().ToString()),
				new Claim("email", email),
			]),
			Issuer = "WebAGK",
			Expires = _now.Add(_options.EmailConfirmExpiry != default ? _options.EmailConfirmExpiry : TimeSpan.FromHours(1)),
			SigningCredentials = _credentials,
		};

		var _handler = new JsonWebTokenHandler();
		var _token = _handler.CreateToken(_tokenDescriptor);

		return _token;
	}

	public string GenerateRefreshToken(Guid userId)
	{
		var _now = _clock.CurrentDate();

		var _tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
			[
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(_now).ToUnixTimeSeconds().ToString()),
			]),
			Issuer = "WebAGK",
			Expires = _now.Add(_options.RefreshExpiry),
			SigningCredentials = _credentials,
		};

		var _handler = new JsonWebTokenHandler();
		var _token = _handler.CreateToken(_tokenDescriptor);

		return _token;
	}


}
