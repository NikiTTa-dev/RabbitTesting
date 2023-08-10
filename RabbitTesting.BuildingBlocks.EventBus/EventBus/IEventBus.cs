using RabbitTesting.BuildingBlocks.EventBus.Events;

namespace RabbitTesting.BuildingBlocks.EventBus.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T: IntegrationEvent;
}