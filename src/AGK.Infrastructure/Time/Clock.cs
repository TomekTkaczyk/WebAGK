using AGK.Domain.Services;

namespace AGK.Infrastructure.Time;
internal class Clock : IClock
{
	public DateTime Current() => DateTime.UtcNow;
}
