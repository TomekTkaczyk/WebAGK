using WebAGK.Shared.Abstractions.Services;
using System.Collections.Concurrent;

namespace WebAGK.Shared.Infrastructure.Services;

public static class EmailsQueue
{
	private static readonly ConcurrentQueue<EmailMessage> _emails = new();

	public static ConcurrentQueue<EmailMessage> Emails => _emails;

	public static int ErrorCount { get; set; } = 0;

	public static void Add(EmailMessage emailMessage)
	{
		_emails.Enqueue(emailMessage);
	}

	public static bool TryDequeue(out EmailMessage emailMessage)
	{
		return _emails.TryDequeue(out emailMessage);
	}


	public static bool TryPeek(out EmailMessage emailMessage)
	{
		return _emails.TryPeek(out emailMessage);
	}

	public static bool IsEmpty => _emails.IsEmpty;
}
