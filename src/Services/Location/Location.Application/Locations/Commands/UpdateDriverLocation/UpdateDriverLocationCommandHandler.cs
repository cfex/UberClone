using Location.Application.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Location.Application.Locations.Commands.UpdateLocation;

public sealed class UpdateDriverLocationCommandHandler : IRequestHandler<UpdateDriverLocationCommand>
{
    private readonly ILogger<UpdateDriverLocationCommandHandler> _logger;
    private readonly ILocationRepository _repository;

    public UpdateDriverLocationCommandHandler(IDatabase cache, ILogger<UpdateDriverLocationCommandHandler> logger,
        ILocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Handle(UpdateDriverLocationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating driver location ${request.Latitude}, {request.Longitude}...");

        await _repository.UpdateDriverLocationAsync(request.DriverId, request.Latitude, request.Longitude);
    }
}