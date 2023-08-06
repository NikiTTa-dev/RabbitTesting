namespace RabbitTesting.Contracts.RabbitMq.Events;

public record RabbitMqTestingEvent
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
}