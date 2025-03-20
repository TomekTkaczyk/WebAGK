using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAGK.Shared.Abstractions.Services;

namespace WebAGK.Shared.Infrastructure.Services;

internal class FakeEmailSender : IEmailSender
{
	private readonly IOptionsMonitor<SmtpOptions> _smtpOptionsMonitor;
	private readonly ILogger<FakeEmailSender> _logger;

	private static Timer _debounceTimer;
	private static readonly object Lock = new();
	private const int DebounceDelay = 1000; // milliseconds


	private static int _counter; 

	public FakeEmailSender(IOptionsMonitor<SmtpOptions> smtpOptionsMonitor, ILogger<FakeEmailSender> logger)
	{
		_smtpOptionsMonitor = smtpOptionsMonitor;
		_logger = logger;
		_smtpOptionsMonitor.OnChange(OnSmtpOptionsChanged);
	}

	private void OnSmtpOptionsChanged(SmtpOptions options, string name)
	{
		lock(Lock) {
			_debounceTimer?.Change(Timeout.Infinite, Timeout.Infinite);
			_debounceTimer = new Timer(_ =>
			{
				lock (Lock) {
					_counter++;
				}
				_logger.LogInformation($"\n[{_counter}] SmtpOptions changed (Name: {name}): {JsonSerializer.Serialize(options)}", _counter);
			}, null, DebounceDelay, Timeout.Infinite);
		}
	}

	public async Task<bool> SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
	{
		await Task.CompletedTask;
		
		return false;
	}
}
