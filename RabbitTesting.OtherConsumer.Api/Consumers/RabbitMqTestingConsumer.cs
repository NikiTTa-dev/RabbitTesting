using MassTransit;
using RabbitTesting.Contracts.RabbitMq.Events;

namespace RabbitTesting.OtherConsumer.Api.Consumers;

public sealed class RabbitMqTestingConsumer: IConsumer<RabbitMqTestingEvent>
{
    private readonly ILogger<RabbitMqTestingConsumer> _logger;

    public RabbitMqTestingConsumer(ILogger<RabbitMqTestingConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<RabbitMqTestingEvent> context)
    {
        _logger.LogInformation("RabbitMqTested: {@Product}", context.Message);
        return Task.CompletedTask;
    }
}