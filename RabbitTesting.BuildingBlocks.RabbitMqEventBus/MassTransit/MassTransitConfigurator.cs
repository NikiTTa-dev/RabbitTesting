using MassTransit;

namespace RabbitTesting.BuildingBlocks.RabbitMqEventBus.MassTransit;

public class MassTransitConfigurator
{
    public Action<IRegistrationConfigurator>? ConfigureBus { get; set; }
    public Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext>? ConfigureRabbitMqBus { get; set; }
    public string RabbitMqSectionName { get; set; } = null!;
}