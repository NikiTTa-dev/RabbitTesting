using MassTransit;
using RabbitTesting.BuildingBlocks.EventBus.EventBus;
using RabbitTesting.BuildingBlocks.EventBus.Events;

namespace RabbitTesting.BuildingBlocks.RabbitMqEventBus.EventBus;

public class RabbitMqEventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqEventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IntegrationEvent =>
        _publishEndpoint.Publish(message, cancellationToken);
}