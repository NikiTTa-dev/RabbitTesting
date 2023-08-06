using MassTransit;
using RabbitTesting.BuildingBlocks.EventBus.EventBus;

namespace RabbitTesting.BuildingBlocks.RabbitMqEventBus.EventBus;

public class RabbitMqEventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqEventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class =>
        _publishEndpoint.Publish(message, cancellationToken);
}