using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitTesting.BuildingBlocks.EventBus.EventBus;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.MassTransit;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.Settings;

namespace RabbitTesting.BuildingBlocks.RabbitMqEventBus.Extensions;

public static class MassTransitServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqMassTransit(
        this IServiceCollection services,
        ConfigurationManager configuration,
        MassTransitConfigurator? massTransitConfigurator)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        if (massTransitConfigurator is null)
            throw new ArgumentNullException(nameof(massTransitConfigurator));
        
        if (massTransitConfigurator.RabbitMqSectionName is null)
            throw new NullReferenceException(nameof(massTransitConfigurator.RabbitMqSectionName));

        var rabbitSettings = configuration.GetSection(massTransitConfigurator.RabbitMqSectionName);
        if (rabbitSettings is null)
            throw new NullReferenceException("Could not find section by given name.");

        services.Configure<RabbitMqSettings>(
            rabbitSettings);

        services.AddTransient<IEventBus, EventBus.RabbitMqEventBus>();
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.ConfigureEndpoints(context);
                var settings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
                
                massTransitConfigurator.ConfigureRabbitMqBus?.Invoke(configurator, context);
            });
            massTransitConfigurator.ConfigureBus?.Invoke(busConfigurator);
        });

        return services;
    }
}