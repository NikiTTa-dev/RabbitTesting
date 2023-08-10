using MassTransit;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.Consumers;
using RabbitTesting.Contracts.RabbitMq.Events;

namespace RabbitTesting.Consumer.Api.Consumers;

public sealed class SecondRabbitMqTestingConsumer : ConsumerBase<NewRabbitMqTestingEvent>
{
    private readonly ILogger<NewRabbitMqTestingEvent> _logger;

    public SecondRabbitMqTestingConsumer(ILogger<NewRabbitMqTestingEvent> logger)
    {
        _logger = logger;
    }

    protected override async Task Handle(ConsumeContext<NewRabbitMqTestingEvent> context)
    {
        await ProduceError(context);
        _logger.LogInformation("RabbitMqTested: {@Product}", context.Message);
    }

    private async Task ProduceError(ConsumeContext<NewRabbitMqTestingEvent> context)
    {
        await Failure(context, "AAAA");
    }
}