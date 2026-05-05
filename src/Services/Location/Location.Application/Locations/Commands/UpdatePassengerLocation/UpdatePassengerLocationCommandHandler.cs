using Location.Application.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Location.Application.Locations.Commands.UpdatePassengerLocation;

public sealed class UpdatePassengerLocationCommandHandler : IRequestHandler<UpdatePassengerLocationCommand>
{
    private readonly ILogger<UpdatePassengerLocationCommandHandler> _logger;
    private readonly ILocationRepository _repository;

    public UpdatePassengerLocationCommandHandler(IDatabase cache, ILogger<UpdatePassengerLocationCommandHandler> logger,
        ILocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Handle(UpdatePassengerLocationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating passenger location ${request.Latitude}, {request.Longitude}...");

        await _repository.UpdatePassengerLocationAsync(request.PassengerId, request.Latitude, request.Longitude);
    }
}