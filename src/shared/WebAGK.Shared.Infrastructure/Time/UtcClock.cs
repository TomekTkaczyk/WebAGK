using WebAGK.Shared.Abstractions;

namespace WebAGK.Shared.Infrastructure.Time;
internal class UtcClock : IClock
{
	public DateTime CurrentDate() => DateTime.UtcNow;
}
