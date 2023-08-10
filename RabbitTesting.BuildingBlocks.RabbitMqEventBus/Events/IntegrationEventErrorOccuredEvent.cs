using RabbitTesting.BuildingBlocks.EventBus.Events;

namespace RabbitTesting.BuildingBlocks.RabbitMqEventBus.Events;

public record IntegrationEventErrorOccuredEvent(
        Guid EventId,
        string ErrorMassage,
        string? StackTrace) 
    : IntegrationEvent;