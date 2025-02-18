using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using WebAGK.Shared.Abstractions.Services;

namespace WebAGK.Shared.Infrastructure.Services;
internal class SmtpEmailSender : IEmailSender
{
	private readonly IOptionsMonitor<SmtpOptions> _optionsMonitor;
	private readonly ILogger<IEmailSender> _logger;

	public SmtpEmailSender(IOptionsMonitor<SmtpOptions> optionsMonitor, ILogger<IEmailSender> logger)
	{
		_logger = logger;
		_optionsMonitor = optionsMonitor;
		_optionsMonitor.OnChange(OnConfigurationChanged);
		if(!IsValidConfiguration(_optionsMonitor.CurrentValue, out var errors)) {
			_logger.LogError($"\n[{errors.Count}] Invalid SMTP configuration detected: {errors}", string.Join(", ", errors));
		}
	}

	public async Task<bool> SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
	{
		var options = _optionsMonitor.CurrentValue;
		if(!IsValidConfiguration(options, out var errors)) {
			_logger.LogError($"\nInvalid SMTP configuration detected: {string.Join(", ", errors)}");
			return false;
		}

		using var client = new SmtpClient();
		try {
			await client.ConnectAsync(
				host: options.Host,
				port: options.Port,
				options: SecureSocketOptions.SslOnConnect,
				cancellationToken);
			await client.AuthenticateAsync(
				_optionsMonitor.CurrentValue.Account, 
				_optionsMonitor.CurrentValue.Password, 
				cancellationToken);
			await client.SendAsync(CreateEmail(emailMessage), cancellationToken);
			await client.DisconnectAsync(true, cancellationToken);
		}
		catch (Exception ex) {
			_logger.LogError(ex, ex.Message, client);
			throw;
		}

		return true;
	}

	private void OnConfigurationChanged(SmtpOptions newOptions)
	{
		_logger.LogInformation("OnConfigurationChanged invoked.");
		if(!IsValidConfiguration(newOptions, out var errors)) {
			_logger.LogError("Invalid SMTP configuration detected: {Errors}", string.Join(", ", errors));
		}
		else {
			_logger.LogInformation("SMTP configuration updated successfully and is valid.");
		}
	}

	private static bool IsValidConfiguration(SmtpOptions options, out List<string> errors)
	{
		errors = [];
		if(string.IsNullOrEmpty(options.Host)) {
			errors.Add("SMTP host required.");
		}
		if(options.Port <= 0) {
			errors.Add("SMTP port must by a positive number.");
		}
		if(string.IsNullOrWhiteSpace(options.Password)) {
			errors.Add("SMTP Password is required.");
		}
		if(string.IsNullOrWhiteSpace(options.Issuer)) {
			errors.Add("SMTP Issuer is required.");
		}
		if(string.IsNullOrWhiteSpace(options.IssuerEmail)) {
			errors.Add("SMTP IssuerEmail is required.");
		}

		return errors.Count == 0;
	}

	private MimeMessage CreateEmail(EmailMessage emailMessage)
	{
		var bodyBuilder = new BodyBuilder
		{
			HtmlBody = emailMessage.Body,
			TextBody = @"Text body do rozważenia"
		};

		var message = new MimeMessage()
		{
			Subject = emailMessage.Subject,
			Body = bodyBuilder.ToMessageBody()
		};
		message.From.Add(new MailboxAddress(_optionsMonitor.CurrentValue.Issuer, _optionsMonitor.CurrentValue.IssuerEmail));
		foreach(var address in emailMessage.Recievers) {
			message.To.Add(new MailboxAddress("", address));
		}

		return message;
	}
}
