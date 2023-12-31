﻿using RabbitTesting.BuildingBlocks.EventBus.Events;

namespace RabbitTesting.Contracts.RabbitMq.Events;

public record RabbitMqTestingEvent : IntegrationEvent
{
    public long ItemId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
}