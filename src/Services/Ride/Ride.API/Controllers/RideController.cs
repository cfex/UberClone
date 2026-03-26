using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ride.API.Services;

namespace Ride.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RideController : ControllerBase
{
    private readonly IDriverGrpcClient _driverGrpcClient;
    private readonly ILogger<RideController> _logger;

    public RideController(
        IDriverGrpcClient driverGrpcClient,
        ILogger<RideController> logger)
    {
        _driverGrpcClient = driverGrpcClient;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<Unit>> Hello(
        CancellationToken cancellationToken)
    {
        return Ok();
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
            vehicleType = driverInfo.VehicleType,
            licensePlate = driverInfo.LicensePlate
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