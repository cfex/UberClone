using MediatR;
using Ride.Domain.Enums;
using Ride.Domain.Repositories;
using Ride.Domain.ValueObjects;

namespace Ride.Application.Rides.Commands.RequestRide;

public class RequestRideCommandHandler : IRequestHandler<RequestRideCommand, Guid>
{
    private readonly IRideRepository _rideRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RequestRideCommandHandler(IUnitOfWork unitOfWork, IRideRepository rideRepository)
    {
        _unitOfWork = unitOfWork;
        _rideRepository = rideRepository;
    }

    public async Task<Guid> Handle(RequestRideCommand request, CancellationToken cancellationToken)
    {
        // var pickupLocation = call location service 
        var location = Location.RandomLocation();
        var destination = Location.RandomLocation();

        var ride = Domain.Entities.Ride.Create(null,
            request.PassengerId,
            Location.Create(location.Longitude, location.Latitude),
            Location.Create(destination.Longitude, destination.Latitude),
            RideStatus.Requested, DateTime.UtcNow);


        await _rideRepository.CreateAsync(ride, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ride.Id;
    }
}