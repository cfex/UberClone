using FluentValidation;

namespace Ride.Application.Rides.Commands.RequestRide;

public sealed class RequestRideCommandValidator : AbstractValidator<RequestRideCommand>
{
    public RequestRideCommandValidator()
    {
        RuleFor(x => x.PassengerId)
            .NotEmpty().WithMessage("Passenger ID is required");

        RuleFor(x => x.Destination)
            .NotNull().WithMessage("Destination is required");

        RuleFor(x => x.Destination.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Destination latitude must be between -90 and 90")
            .When(x => x.Destination != null);

        RuleFor(x => x.Destination.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Destination longitude must be between -180 and 180")
            .When(x => x.Destination != null);
    }
}
