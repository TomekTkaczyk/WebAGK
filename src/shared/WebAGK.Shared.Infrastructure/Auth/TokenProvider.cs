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
		var now = _clock.CurrentDate();
		var formattedClaims = new Dictionary<string, object>();

		if(claims is not null) {
			foreach(var claim in claims) {
				if(claim.Value is IEnumerable<string> enumerable) {
					formattedClaims[claim.Key] = enumerable.Count() == 1 ? enumerable.First() : enumerable.ToArray();
				}
				else {
					formattedClaims[claim.Key] = claim.Value;
				}
			}
		}

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
			[
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
			]),
			Issuer = "WebAGK",
			Expires = now.Add(_options.Expiry),
			SigningCredentials = _credentials,
			Claims = formattedClaims,
		};

		var handler = new JsonWebTokenHandler();
		var token = handler.CreateToken(tokenDescriptor);

		return token;
	}

	public string GenerateConfirmEmailToken(Guid userId, string email)
	{
		var now = _clock.CurrentDate();

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
			[
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
				new Claim("email", email),
			]),
			Issuer = "WebAGK",
			Expires = now.Add(_options.EmailConfirmExpiry != default ? _options.EmailConfirmExpiry : TimeSpan.FromHours(1)),
			SigningCredentials = _credentials,
		};

		var handler = new JsonWebTokenHandler();
		var token = handler.CreateToken(tokenDescriptor);

		return token;
	}

	public string GenerateRefreshToken(Guid userId)
	{
		var now = _clock.CurrentDate();

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
			[
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
			]),
			Issuer = "WebAGK",
			Expires = now.Add(_options.RefreshExpiry),
			SigningCredentials = _credentials,
		};

		var handler = new JsonWebTokenHandler();
		var token = handler.CreateToken(tokenDescriptor);

		return token;
	}


}
