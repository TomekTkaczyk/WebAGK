using WebAGK.Shared.Infrastructure.Auth;
using WebAGK.Shared.Infrastructure.Time;

namespace InfrastructureTests.Auth;

public class JwtEmailConfirmerTests
{
	[Fact]
	public void GetConfirmEmailBody_should_by_return_valid_body()
	{
		var _clock = new UtcClock();
		var _options = new AuthOptions
		{
			EmailConfirmExpiry = TimeSpan.FromDays(1),
			IssuerSigningKey = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
		};
		var _id = Guid.NewGuid();
		var _confirmer = new JwtEmailConfirmer(_options, _clock);


		var _body = _confirmer.GetConfirmEmailBody(_id, "sample@mail.it","");

		Assert.NotNull(_body);
	}
}
