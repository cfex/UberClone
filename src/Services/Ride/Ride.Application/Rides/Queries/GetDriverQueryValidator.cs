using FluentValidation;

namespace Ride.Application.Rides.Queries;

public sealed class GetDriverQueryValidator : AbstractValidator<GetDriverQuery>
{
    public GetDriverQueryValidator()
    {
        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("Driver ID is required");
    }
}
