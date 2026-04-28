using Location.API.Dto;
using Location.Application.Locations.Commands.UpdateLocation;
using Location.Application.Locations.Commands.UpdatePassengerLocation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Location.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly IMediator _mediator;

    public LocationController(ILogger<LocationController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPut("/driver")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Unit>> UpdateDriverLocation([FromBody] LocationUpdateRequestDto req)
    {
        _logger.LogInformation("Updating driver location");

        // TODO: implement keycloak to get real auth user id
        var command = new UpdateDriverLocationCommand(Guid.NewGuid(), req.Latitude, req.Longitude);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("/passenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Unit>> UpdateLocation([FromBody] LocationUpdateRequestDto req)
    {
        _logger.LogInformation("Updating passenger location");

        // TODO: implement keycloak to get real auth user id
        var command = new UpdatePassengerLocationCommand(Guid.NewGuid(), req.Latitude, req.Longitude);
        await _mediator.Send(command);
        return Ok();
    }
}