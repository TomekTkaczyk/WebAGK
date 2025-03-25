using MediatR;

namespace WebAGK.Shared.Abstractions.Messaging;

public interface IMessage : INotification {
    Guid MessageId { get; }
    DateTime OccurredOn { get; }
    string Type { get; }
    string Topic { get; }
}