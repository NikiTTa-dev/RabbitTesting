using MassTransit;
using RabbitTesting.BuildingBlocks.EventBus.Events;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.Events;

namespace RabbitTesting.BuildingBlocks.RabbitMqEventBus.Consumers;

public abstract class ConsumerBase<T> : IConsumer<T>
    where T : IntegrationEvent
{
    public async Task Consume(ConsumeContext<T> context)
    {
        try
        {
            await Handle(context);
        }
        catch (Exception ex)
        {
            await PublishError(context, ex.Message, ex.StackTrace);
        }
    }

    protected abstract Task Handle(ConsumeContext<T> context);

    protected virtual async Task Failure(ConsumeContext<T> context, string? message = null)
    {
        await PublishError(context, message ?? "An error occurred while processing the request.");
    }

    protected async Task PublishError(
        ConsumeContext<T> context,
        string message,
        string? stackTrace = null)
    {
        await context.Publish(new IntegrationEventErrorOccuredEvent
        (
            EventId: context.Message.Id,
            ErrorMassage: message,
            StackTrace: stackTrace
        ));
    } 
}