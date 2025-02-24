using WebAGK.Shared.Infrastructure.Auth;
using WebAGK.Shared.Infrastructure.Time;

namespace InfrastructureTests.Auth;
public class TestFixture
{
	internal UtcClock Clock { get; }
	internal AuthOptions AuthOptions { get; }

	public TestFixture()
	{
		Clock = new UtcClock();
		AuthOptions = new AuthOptions()
		{
			IssuerSigningKey = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
		};
	}
}
