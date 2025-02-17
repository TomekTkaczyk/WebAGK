using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAGK.Shared.Abstractions.Services;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace WebAGK.Shared.Infrastructure.Services;

internal class FakeEmailSender : IEmailSender
{
	private readonly IOptionsMonitor<SmtpOptions> _smtpOptionsMonitor;
	private readonly ILogger<FakeEmailSender> _logger;

	private static Timer _debounceTimer;
	private static readonly object _lock = new();
	private const int DebounceDelay = 1000; // milliseconds


	public static int Counter = 0; 

	public FakeEmailSender(IOptionsMonitor<SmtpOptions> smtpOptionsMonitor, ILogger<FakeEmailSender> logger)
	{
		_smtpOptionsMonitor = smtpOptionsMonitor;
		_logger = logger;
		_smtpOptionsMonitor.OnChange(OnSmtpOptionsChanged);
	}

	private void OnSmtpOptionsChanged(SmtpOptions options, string name)
	{
		lock(_lock) {

		_debounceTimer?.Change(Timeout.Infinite, Timeout.Infinite);
		_debounceTimer = new Timer(_ =>
		{
			lock(_lock)
			Counter++;
			_logger.LogInformation($"\n[{Counter}] SmtpOptions changed (Name: {name}): {JsonSerializer.Serialize(options)}", Counter);
		}, null, DebounceDelay, Timeout.Infinite);
		}
	}

	public async Task<bool> SendEmailAsync(Email email, CancellationToken cancellationToken = default)
	{
		await Task.CompletedTask;
		
		return false;
	}
}
