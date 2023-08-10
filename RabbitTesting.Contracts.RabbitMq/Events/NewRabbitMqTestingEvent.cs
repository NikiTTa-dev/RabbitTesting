using RabbitTesting.BuildingBlocks.EventBus.Events;

namespace RabbitTesting.Contracts.RabbitMq.Events;

public record NewRabbitMqTestingEvent : IntegrationEvent
{
    public long ItemId { get; init; }
}