using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ride.API.Dto;
using Ride.Application.Rides.Commands.RequestRide;
using Ride.Application.Rides.Queries;

namespace Ride.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RideController : ControllerBase
{
    private readonly ILogger<RideController> _logger;
    private readonly IMediator _mediator;

    public RideController(
        IMediator mediator,
        ILogger<RideController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestRide([FromBody] CreateRideRequestDto request,
        CancellationToken cancellationToken)
    {
        var command = new RequestRideCommand(Guid.Parse(request.PassengerId), request.Destination);
        var response = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Ride is requested");

        return CreatedAtAction(nameof(GetRideInfo), new { rideId = response }, response);
    }

    [HttpGet("{rideId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RideDetailsDto>> GetRideInfo(string rideId, CancellationToken cancellationToken)
    {
        var command = new GetRideDetailsQuery(Guid.Parse(rideId));
        var rideDetails = await _mediator.Send(command, cancellationToken);

        return Ok(rideDetails);
    }

    [HttpGet("driver/{driverId}")]
    public async Task<IActionResult> GetDriverInfo(string driverId)
    {
        _logger.LogInformation("Getting driver info for: {DriverId}", driverId);

        var command = new GetDriverQuery(Guid.Parse(driverId));
        var driverInfo = await _mediator.Send(command);

        if (driverInfo is null)
            throw new Exception($"Driver {driverId} not found");

        return Ok(new
        {
            driverId = driverInfo.DriverId,
            name = driverInfo.Name,
            status = driverInfo.Status,
            fare = driverInfo.Fare
        });
    }
}