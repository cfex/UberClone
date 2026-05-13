using FluentValidation;

namespace Ride.Application.Rides.Commands.AcceptRide;

public sealed class AcceptRideRequestCommandValidator : AbstractValidator<AcceptRideCommand>
{
    public AcceptRideRequestCommandValidator()
    {
        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("DriverId is required");
    }
}