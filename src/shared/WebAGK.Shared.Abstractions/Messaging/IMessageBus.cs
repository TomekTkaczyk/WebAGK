namespace WebAGK.Shared.Abstractions.Messaging;

public interface IMessageBus {
    Task PublishAsync<TMessage>(TMessage messages) where TMessage : IMessage;
}