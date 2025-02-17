using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAGK.Shared.Infrastructure.Auth;
using WebAGK.Shared.Infrastructure.Time;
using System.IdentityModel.Tokens.Jwt;

namespace InfrastructureTests.Auth;

public class JwtEmailConfirmerTests
{
	[Fact]
	public void GetConfirmEmailBody_should_by_return_valid_body()
	{
		var clock = new UtcClock();
		var options = new AuthOptions
		{
			EmailConfirmExpiry = TimeSpan.FromDays(1),
			IssuerSigningKey = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
		};
		var id = Guid.NewGuid();
		var confirmer = new JwtEmailConfirmer(options, clock);


		var body = confirmer.GetConfirmEmailBody(id, "sample@mail.it","");

		Assert.NotNull(body);
	}
}
