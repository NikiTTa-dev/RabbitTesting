using MassTransit;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.Extensions;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.MassTransit;
using RabbitTesting.Consumer.Api.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRabbitMqMassTransit(builder.Configuration, new MassTransitConfigurator
{
    RabbitMqSectionName = "Settings",
    ConfigureBus = (configurator) =>
    {
        configurator.AddConsumers(typeof(Program).Assembly);
        configurator.AddConsumer<RabbitMqTestingConsumer>()
            .Endpoint(registrationConfigurator => registrationConfigurator.Name = "rabbit-mq-testing-logging");
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();