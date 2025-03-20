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
		if(!IsValidConfiguration(_optionsMonitor.CurrentValue, out var _errors)) {
			_logger.LogError($"\n[{_errors.Count}] Invalid SMTP configuration detected: {_errors}", string.Join(", ", _errors));
		}
	}

	public async Task<bool> SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
	{
		var _options = _optionsMonitor.CurrentValue;
		if(!IsValidConfiguration(_options, out var _errors)) {
			_logger.LogError($"\nInvalid SMTP configuration detected: {string.Join(", ", _errors)}");
			return false;
		}

		using var _client = new SmtpClient();
		try {
			await _client.ConnectAsync(
				host: _options.Host,
				port: _options.Port,
				options: SecureSocketOptions.SslOnConnect,
				cancellationToken);
			await _client.AuthenticateAsync(
				_optionsMonitor.CurrentValue.Account, 
				_optionsMonitor.CurrentValue.Password, 
				cancellationToken);
			await _client.SendAsync(CreateEmail(emailMessage), cancellationToken);
			await _client.DisconnectAsync(true, cancellationToken);
		}
		catch (Exception _ex) {
			_logger.LogError(_ex, _ex.Message, _client);
			throw;
		}

		return true;
	}

	private void OnConfigurationChanged(SmtpOptions newOptions)
	{
		_logger.LogInformation("OnConfigurationChanged invoked.");
		if(!IsValidConfiguration(newOptions, out var _errors)) {
			_logger.LogError("Invalid SMTP configuration detected: {Errors}", string.Join(", ", _errors));
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
		var _bodyBuilder = new BodyBuilder
		{
			HtmlBody = emailMessage.Body,
			TextBody = @"Text body do rozważenia"
		};

		var _message = new MimeMessage()
		{
			Subject = emailMessage.Subject,
			Body = _bodyBuilder.ToMessageBody()
		};
		_message.From.Add(new MailboxAddress(_optionsMonitor.CurrentValue.Issuer, _optionsMonitor.CurrentValue.IssuerEmail));
		foreach(var _address in emailMessage.Recievers) {
			_message.To.Add(new MailboxAddress("", _address));
		}

		return _message;
	}
}
