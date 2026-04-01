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
        var location = RandomLocation();
        var passengerId = Guid.NewGuid(); // mock before passenger service
        var ride = Domain.Entities.Ride.Create(Guid.NewGuid(), passengerId,
            Location.Create(location.Longitude, location.Latitude),
            request.Destination, RideStatus.Requested);


        await _rideRepository.CreateAsync(ride, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ride.Id;
    }

    private static (double Latitude, double Longitude) RandomLocation()
    {
        var random = new Random();
        var latitude = random.NextDouble() * 180 - 90;
        var longitude = random.NextDouble() * 360 - 180;
        return (latitude, longitude);
    }
}