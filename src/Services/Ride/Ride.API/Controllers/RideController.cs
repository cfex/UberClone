using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ride.API.Dtos;
using Ride.API.Services;
using Ride.Application.Rides.Commands.RequestRide;

namespace Ride.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RideController : ControllerBase
{
    private readonly IDriverGrpcClient _driverGrpcClient;
    private readonly IMediator _mediator;
    private readonly ILogger<RideController> _logger;

    public RideController(
        IDriverGrpcClient driverGrpcClient,
        ILogger<RideController> logger)
    {
        _driverGrpcClient = driverGrpcClient;
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

        var driverInfo = await _driverGrpcClient.GetDriverInfoAsync(driverId);

        if (driverInfo == null) return NotFound(new { message = "Driver not found or service unavailable" });

        return Ok(new
        {
            driverId = driverInfo.DriverId,
            name = driverInfo.Name,
            status = driverInfo.Status,
            fare = driverInfo.Fare
        });
    }

    [HttpGet("driver/{driverId}/available")]
    public async Task<IActionResult> CheckDriverAvailability(string driverId)
    {
        _logger.LogInformation("Checking availability for driver: {DriverId}", driverId);

        var isAvailable = await _driverGrpcClient.IsDriverAvailableAsync(driverId);

        return Ok(new { driverId, isAvailable });
    }
}