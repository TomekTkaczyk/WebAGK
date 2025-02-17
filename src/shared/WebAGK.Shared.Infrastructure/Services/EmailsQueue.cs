using WebAGK.Shared.Abstractions.Services;
using System.Collections.Concurrent;

namespace WebAGK.Shared.Infrastructure.Services;

public static class EmailsQueue
{
	private static readonly ConcurrentQueue<Email> _emails = new();

	public static ConcurrentQueue<Email> Emails => _emails;

	public static int ErrorCount { get; set; } = 0;

	public static void Add(Email email)
	{
		_emails.Enqueue(email);
	}

	public static bool TryDequeue(out Email email)
	{
		return _emails.TryDequeue(out email);
	}


	public static bool TryPeek(out Email email)
	{
		return _emails.TryPeek(out email);
	}

	public static bool IsEmpty => _emails.IsEmpty;
}
