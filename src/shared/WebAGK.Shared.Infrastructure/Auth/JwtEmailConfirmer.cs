using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.Auth;

internal class JwtEmailConfirmer(AuthOptions options, IClock clock) : IEmailConfirmer
{
	public string ConfirmToken { get; private set; }

	public string GetConfirmToken(Guid userId, string email)
	{
		CreateToken(userId, email);
		return ConfirmToken;
	}

	public string GetRemindPasswordBody(Guid userId, string email)
	{
		CreateToken(userId, email);

		return ConfirmToken;
	}

	public string GetConfirmEmailBody(Guid userId, string email, string confirmUrl)
	{
		CreateToken(userId, email);

		_ = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://localhost:5000";


		return ConfirmToken;
	}

	public bool Confirm(string expected, string received, string email)
	{
		if(!expected.Equals(received)) {
			return false;
		}

		var handler = new JwtSecurityTokenHandler();
		var token = handler.ReadJwtToken(received) 
			?? throw new InvalidEmailTokenException();

		if(token.ValidTo < clock.CurrentDate()){
			throw new InvalidEmailTokenException();
		}

		var claim = token.Claims.FirstOrDefault(x => x.Type.Equals("email"));
		var emailFromToken = claim.Value;
		if(emailFromToken is null || !emailFromToken.Equals(email)) {
			throw new InvalidEmailTokenException();
		}

		return true;
	}

	private string CreateToken(Guid userId, string email)
	{
		var now = clock.CurrentDate();
		if(options.EmailConfirmExpiry.TotalSeconds < 1) {
			options.EmailConfirmExpiry = TimeSpan.FromDays(1);
		}
		var expires = now.Add(options.EmailConfirmExpiry);
		var claims = new List<Claim> {
			new("id", userId.ToString()),
			new("email", email),
			new("code", GenerateVerificationCode())
		};

		var jwt = new JwtSecurityToken(
			userId.ToString(),
			expires: expires,
			claims: claims);

		ConfirmToken = new JwtSecurityTokenHandler().WriteToken(jwt);

		return ConfirmToken;
	}

	private static string GenerateVerificationCode()
	{
		var rnd = new Random();
		return rnd.Next(100000, 999999).ToString();
	}
}
