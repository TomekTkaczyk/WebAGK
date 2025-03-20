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
			catch(Exception _ex) {
				logger.LogError(_ex, "Error processing email.");
			}
		}
	}

	private async Task ProcessEmailAsync(CancellationToken cancellationToken)
	{
		await Task.Run(async () =>
		{
			if(EmailsQueue.ErrorCount <= emailServiceFactory.RetryCountLimit && EmailsQueue.TryDequeue(out var _email)) {
				var _emailService = emailServiceFactory.GetEmailSender();
				var _isEmailSent = await _emailService.SendEmailAsync(_email, cancellationToken);
				if(!_isEmailSent) {
					if(++EmailsQueue.ErrorCount > emailServiceFactory.RetryCountLimit) {
						logger.LogError("Error sending email {Subject}.", _email.Subject);
					} else {
						EmailsQueue.Add(_email);
					}
				}
				else {
					logger.LogInformation("EmailMessage {Subject} sent.", _email.Subject);
					EmailsQueue.ErrorCount = 0;
				}
			}
			else {
				await Task.CompletedTask;
			}
		}, cancellationToken);
	}
}
