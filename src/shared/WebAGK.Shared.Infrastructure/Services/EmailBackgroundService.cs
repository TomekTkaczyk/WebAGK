using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAGK.Shared.Abstractions.Services;

namespace WebAGK.Shared.Infrastructure.Services;
internal class EmailBackgroundService(IEmailSenderFactory emailServiceFactory, ILogger<EmailBackgroundService> logger) : BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		while(!cancellationToken.IsCancellationRequested) {
			try {
				if(!EmailsQueue.IsEmpty) {
					await ProcessEmailAsync(cancellationToken);
				}
				await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
			}
			catch(Exception ex) {
				logger.LogError(ex, "Error processing email.");
			}
		}
	}

	private async Task ProcessEmailAsync(CancellationToken cancellationToken)
	{
		await Task.Run(async () =>
		{
			if(EmailsQueue.ErrorCount <= emailServiceFactory.RetryCountLimit && EmailsQueue.TryDequeue(out var email)) {
				var emailService = emailServiceFactory.GetEmailSender();
				var isEmailSent = await emailService.SendEmailAsync(email, cancellationToken);
				if(!isEmailSent) {
					if(++EmailsQueue.ErrorCount > emailServiceFactory.RetryCountLimit) {
						logger.LogError("Error sending email {Subject}.", email.Subject);
					} else {
						EmailsQueue.Add(email);
					}
				}
				else {
					logger.LogInformation("Email {Subject} sent.", email.Subject);
					EmailsQueue.ErrorCount = 0;
				}
			}
			else {
				await Task.CompletedTask;
			}
		}, cancellationToken);
	}
}
