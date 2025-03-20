using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Abstractions.Services;

namespace WebAGK.Shared.Infrastructure.Auth;

public class EmailConfirmerFactory(AuthOptions options, IClock clock) : IEmailConfirmerFactory
{
	public IEmailConfirmer GetEmailConfirmer()
	{
		_ = Enum.TryParse(options.EmailConfirmType, true, out EmailConfirmTypes _confirmType);

		return GetEmailConfirmer(_confirmType);
	}

	public IEmailConfirmer GetEmailConfirmer(EmailConfirmTypes confirmTypes)
	{
		return confirmTypes switch
		{
			EmailConfirmTypes.Jwt => new JwtEmailConfirmer(options, clock),
			EmailConfirmTypes.Code => new CodeEmailConfirmer(),
			_ => null,
		};
	}
}