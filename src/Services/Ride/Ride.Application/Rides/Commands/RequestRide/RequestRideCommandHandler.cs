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
        // call location service and calculate the price based on destination and pickup location
        // get all available drivers
        // publish an event

        // var pickupLocation = call location service 
        var ride = Domain.Entities.Ride.Create(Guid.NewGuid(), request.PassengerId, request.Destination,
            request.Destination, RideStatus.Requested, Money.Create(0.0, Currency.EUR));


        await _rideRepository.CreateAsync(ride, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ride.Id;
    }
}