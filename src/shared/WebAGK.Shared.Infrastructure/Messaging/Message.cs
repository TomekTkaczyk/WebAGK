using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Messaging;

namespace WebAGK.Shared.Infrastructure.Messaging;

public abstract record Message(string Type, string Topic) : IMessage {
    private static Lazy<IClock> _clock = new(() => throw new InvalidOperationException("Clock is not initialized."));
    public static void InitializeClock(IClock clock) => _clock = new Lazy<IClock>(() => clock);
    public Guid MessageId { get; init; } = Guid.NewGuid();
    public DateTime OccurredOn { get; init; } = _clock.Value.CurrentDate();
}