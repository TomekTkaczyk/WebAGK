using WebAGK.Module.Users.Core.DTO;

namespace WebAGK.Module.Users.Core.Services;
public interface IEmailVerificationService
{
	Task SendSample(CancellationToken cancellationToken);
	Task Confirm(ConfirmEmailDto dto, CancellationToken cancellationToken);
}
