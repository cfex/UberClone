using FluentValidation;

namespace Ride.Application.Rides.Queries;

public sealed class GetRideDetailsQueryValidator : AbstractValidator<GetRideDetailsQuery>
{
    public GetRideDetailsQueryValidator()
    {
        RuleFor(x => x.RideId)
            .NotEmpty().WithMessage("Ride ID is required");
    }
}
