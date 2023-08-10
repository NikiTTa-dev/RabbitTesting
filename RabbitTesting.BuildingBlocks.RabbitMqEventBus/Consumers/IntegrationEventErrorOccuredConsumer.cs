using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.Events;

namespace RabbitTesting.BuildingBlocks.RabbitMqEventBus.Consumers;

public class IntegrationEventErrorOccuredConsumer : IConsumer<IntegrationEventErrorOccuredEvent>
{
    private readonly ILogger<IntegrationEventErrorOccuredConsumer> _logger;

    public IntegrationEventErrorOccuredConsumer(
        ILogger<IntegrationEventErrorOccuredConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<IntegrationEventErrorOccuredEvent> context)
    {
        _logger.LogWarning("IntegrationEvent error occured. EventId: {0}, Error message: {1}",
            context.Message.EventId,
            context.Message.ErrorMassage);

        return Task.CompletedTask;
    }
}