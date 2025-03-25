using MediatR;
using WebAGK.Shared.Abstractions.Messaging;

namespace WebAGK.Shared.Infrastructure.Messaging.Brokers;

internal class MessageBus(IPublisher publisher) : IMessageBus {
    
    public async Task PublishAsync<TMessage>(TMessage messages) where TMessage : IMessage {
        await publisher.Publish(messages);
    }
}