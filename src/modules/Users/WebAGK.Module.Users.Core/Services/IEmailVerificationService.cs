using WebAGK.Module.Users.Core.DTO;

namespace WebAGK.Module.Users.Core.Services;
public interface IEmailVerificationService
{
	Task Confirm(ConfirmEmailDto dto, CancellationToken cancellationToken);
}
