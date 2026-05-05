using MediatR;
using Ride.Application.Abstractions;
using Ride.Domain.Enums;
using Ride.Domain.Repositories;
using Ride.Domain.ValueObjects;

namespace Ride.Application.Rides.Commands.RequestRide;

public class RequestRideCommandHandler : IRequestHandler<RequestRideCommand, Guid>
{
    private readonly ILocationGrpcClient _client;
    private readonly IRideRepository _rideRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RequestRideCommandHandler(IUnitOfWork unitOfWork, IRideRepository rideRepository, ILocationGrpcClient client)
    {
        _unitOfWork = unitOfWork;
        _rideRepository = rideRepository;
        _client = client;
    }

    public async Task<Guid> Handle(RequestRideCommand request, CancellationToken cancellationToken)
    {
        var destination = Location.Create(request.Destination.Longitude, request.Destination.Latitude);

        var passengerCurrentLocation = await _client.GetPassengerLocationAsync(request.PassengerId);
        if (passengerCurrentLocation == null)
            throw new ApplicationException($"Passenger location {request.PassengerId} not found");

        var location = Location.Create(passengerCurrentLocation.Longitude, passengerCurrentLocation.Latitude);

        var ride = Domain.Entities.Ride.Create(
            null,
            request.PassengerId,
            location,
            destination,
            RideStatus.Requested, DateTime.UtcNow);


        await _rideRepository.CreateAsync(ride, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ride.Id;
    }
}