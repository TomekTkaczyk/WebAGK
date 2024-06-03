using AGK.Application.Dto;
using AGK.Application.Services;
using AGK.Domain.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AGK.Infrastructure.Auth;
internal class Authenticator(IOptions<AuthOptions> options, IClock clock) : IAuthenticator
{
	private readonly IClock _clock = clock;
	private readonly string _issuer = options.Value.Issuer;
	private readonly string _audience = options.Value.Audience;
	private readonly TimeSpan _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
	private readonly SigningCredentials _signingCredentials = new(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
			SecurityAlgorithms.HmacSha256);
	private	readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

	public JwtDTO CreateToken(int userId, string role) {

		var now = _clock.Current();
		var expires = now.Add(_expiry);
		
		var claims = new List<Claim> {
			new(JwtRegisteredClaimNames.Sub, userId.ToString()),
			new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
			new(ClaimTypes.Role, role)
		};

		var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
		var accessToken = _jwtSecurityTokenHandler.WriteToken(jwt);

		return new JwtDTO { AccessToken =  accessToken };
	}
}
