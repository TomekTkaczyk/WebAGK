namespace WebAGK.Shared.Abstractions.Services;

public interface IEmailSenderFactory
{
	IEmailSender GetEmailSender();

	int RetryCountLimit { get; }
}
