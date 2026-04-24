using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ride.API.Dtos;
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
    public async Task<IActionResult> RequireRide([FromBody] RideRequestDto request)
    {
        var command = new RequestRideCommand(Guid.Parse(request.PassengerId), request.Destination);
        await _mediator.Send(command);

        _logger.LogInformation("Ride is requested");

        return Ok(new { message = "Ride is requested" });
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