using MassTransit;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.Consumers;
using RabbitTesting.Contracts.RabbitMq.Events;

namespace RabbitTesting.OtherConsumer.Api.Consumers;

public sealed class RabbitMqTestingConsumer: ConsumerBase<RabbitMqTestingEvent>
{
    private readonly ILogger<RabbitMqTestingConsumer> _logger;

    public RabbitMqTestingConsumer(ILogger<RabbitMqTestingConsumer> logger)
    {
        _logger = logger;
    }

    protected override Task Handle(ConsumeContext<RabbitMqTestingEvent> context)
    {
        _logger.LogInformation("RabbitMqTested: {@Product}", context.Message);
        return Task.CompletedTask;
    }
}