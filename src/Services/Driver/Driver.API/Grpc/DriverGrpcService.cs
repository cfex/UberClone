using Driver.Application.Drivers.Commands.GetDriverInfo;
using Grpc.Core;
using MediatR;

namespace Driver.API.Grpc;

public class DriverGrpcService : DriverService.DriverServiceBase
{
    private readonly ILogger<DriverGrpcService> _logger;
    private readonly IMediator _mediator;

    public DriverGrpcService(ILogger<DriverGrpcService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public override async Task<DriverInfoResponse> GetDriverInfo(
        GetDriverInfoRequest request,
        ServerCallContext context)
    {
        var driver = await _mediator.Send(new GetDriverInfoCommand(request.DriverId));

        if (driver == null) throw new RpcException(new Status(StatusCode.NotFound, "Driver not found"));

        return new DriverInfoResponse
        {
            DriverId = driver.Id.ToString(),
            Name = driver.FirstName,
            VehicleType = "",
            LicensePlate = ""
        };
    }

    public override Task<IsDriverAvailableResponse> IsDriverAvailable(
        IsDriverAvailableRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("Checking availability for driver: {DriverId}", request.DriverId);

        // TODO: Proveriti u bazi da li je vozac dostupan
        var response = new IsDriverAvailableResponse
        {
            IsAvailable = true
        };

        return Task.FromResult(response);
    }
}