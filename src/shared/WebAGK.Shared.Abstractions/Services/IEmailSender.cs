namespace WebAGK.Shared.Abstractions.Services;
public interface IEmailSender
{
	Task<bool> SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken);
}
