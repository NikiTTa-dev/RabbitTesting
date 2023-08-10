using Microsoft.AspNetCore.Mvc;
using RabbitTesting.BuildingBlocks.EventBus.EventBus;
using RabbitTesting.Contracts.RabbitMq.Events;

namespace RabbitTesting.Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class RabbitMqTestingController : ControllerBase
{
    private readonly IEventBus _eventBus;

    public RabbitMqTestingController(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        await _eventBus.PublishAsync(new RabbitMqTestingEvent {
            ItemId = 123,
            Name = "RabbitMqTestingEventName",
            Price = 1234
        }, ct);

        await _eventBus.PublishAsync(new NewRabbitMqTestingEvent
        {
            ItemId = 321
        }, ct);
        
        return Ok();
    }
}