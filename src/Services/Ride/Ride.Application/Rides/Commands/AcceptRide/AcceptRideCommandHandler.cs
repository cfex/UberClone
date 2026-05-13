using MediatR;
using Ride.Application.Abstractions;
using Ride.Domain.Enums;
using Ride.Domain.Repositories;
using Ride.Domain.ValueObjects;

namespace Ride.Application.Rides.Commands.AcceptRide;

public class AcceptRideCommandHandler : IRequestHandler<AcceptRideCommand, Guid>
{
    // private readonly IPassengerGrpcClient _passenger;
    private const int MINIMUM_PAYABLE_KM = 10;
    private readonly IDriverGrpcClient _driver;
    private readonly ILocationGrpcClient _location;
    private readonly IRideRepository _rideRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptRideCommandHandler(ILocationGrpcClient location, IRideRepository rideRepository,
        IUnitOfWork unitOfWork, IDriverGrpcClient driver)
    {
        _location = location;
        _rideRepository = rideRepository;
        _unitOfWork = unitOfWork;
        _driver = driver;
    }

    public async Task<Guid> Handle(AcceptRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await _rideRepository.GetByIdAsync(request.RideId, cancellationToken);
        if (ride == null)
            throw new ApplicationException("Ride is cancelled or cannot be accepted");

        var driver = await _driver.GetDriverInfoAsync(request.DriverId.ToString());

        // todo: handle this case. probably need to change ride state and push it back in order to find driver again
        if (driver == null)
            // ride.requeue
            throw new ApplicationException("Driver is cancelled or cannot be accepted");

        if (driver.Status != nameof(DriverStatus.Available))
            throw new ApplicationException("Driver is not available");

        // var passenger = _

        var driverLocation = await _location.GetDriverLocationAsync(request.DriverId);
        var passengerLocation = await _location.GetPassengerLocationAsync(ride.PassengerId);

        ride.AssignDriver(Guid.Parse(driver.DriverId));
        ride.SetFinalPrice(CalculateFinalPrice(ride.PickupLocation, ride.Destination, driver.Fare));
        ride.StartRide();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ride.Id;
    }

    private static Money CalculateFinalPrice(Location pickupLocation, Location destination, double pricePerKilometer)
    {
        // todo: check if this is needed here
        if (pricePerKilometer < 1)
            throw new ArgumentException("Price per kilometer must be greater than zero");

        var trip = pickupLocation.DistanceInKilometersTo(destination);
        var price = trip > 1 ? pricePerKilometer * trip : MINIMUM_PAYABLE_KM;

        return Money.Create(price, Currency.EUR);
    }
}