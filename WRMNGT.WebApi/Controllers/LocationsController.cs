using MediatR;
using Microsoft.AspNetCore.Mvc;
using WRMNGT.Domain.Commands.Location;
using WRMNGT.Domain.Models.Response;
using WRMNGT.Domain.Queries.Location;

namespace WRMNGT.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILogger<LocationsController> _logger;
    private readonly IMediator _mediator;

    public LocationsController(ILogger<LocationsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetLocationsResponseDto), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetLocations(CancellationToken token)
    {
        var result = await _mediator.Send(new GetLocationsQuery(), token);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetLocationResponseDto), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetLocation(string id, CancellationToken token)
    {
        var result = await _mediator.Send(new GetLocationQuery() { Id = new Guid(id) }, token);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateLocationResponseDto), StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command,
        CancellationToken token)
    {
        var result = await _mediator.Send(command, token);

        return CreatedAtAction(nameof(CreateLocation), result);
    }
}