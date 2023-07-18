using MediatR;
using Microsoft.AspNetCore.Mvc;
using WRMNGT.Domain.Commands.Location;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;
    private readonly IMediator _mediator;

    public BookingsController(ILogger<BookingsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(MakeBookingResponseDto), StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> MakeBooking([FromBody] MakeBookingCommand command,
        CancellationToken token)
    {
        var result = await _mediator.Send(command, token);

        return CreatedAtAction(nameof(MakeBooking), result);
    }

    [HttpPatch("process")]
    [ProducesResponseType(typeof(ProcessBookingResponseDto), StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> ProcessBooking([FromBody] ProcessBookingCommand command,
        CancellationToken token)
    {
        var result = await _mediator.Send(command, token);

        return NoContent();
    }
}