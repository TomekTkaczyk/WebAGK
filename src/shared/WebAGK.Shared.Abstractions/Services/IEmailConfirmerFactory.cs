using WebAGK.Shared.Abstractions.Auth;

namespace WebAGK.Shared.Abstractions.Services;
public interface IEmailConfirmerFactory
{
	IEmailConfirmer GetEmailConfirmer();
	IEmailConfirmer GetEmailConfirmer(EmailConfirmTypes confirmTypes);
}