using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.Extensions;
using RabbitTesting.BuildingBlocks.RabbitMqEventBus.MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();
builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(2);
    options.Predicate = (check) => check.Tags.Contains("ready");
});

builder.Services.AddRabbitMqMassTransit(builder.Configuration, new MassTransitConfigurator
{
    RabbitMqSectionName = "Settings"
});

// builder.Services.AddMassTransit(busConfigurator =>
// {
//     busConfigurator.SetKebabCaseEndpointNameFormatter();
//     busConfigurator.AddConsumers(typeof(Program).Assembly);
//     
//     busConfigurator.UsingRabbitMq((context, configurator) =>
//     {
//         configurator.ConfigureEndpoints(context);
//         var settings = context.GetRequiredService<RabbitMqSettings>();
//         configurator.Host(new Uri(settings.Host), h =>
//         {
//             h.Username(settings.Username);
//             h.Password(settings.Password);
//         });
//     });
// });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();