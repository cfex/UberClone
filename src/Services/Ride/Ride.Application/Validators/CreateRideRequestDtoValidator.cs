using FluentValidation;
using Ride.Application.Dto;

namespace Ride.Application.Validators;

public class CreateRideRequestDtoValidator : AbstractValidator<CreateRideRequestDto>
{
    public CreateRideRequestDtoValidator()
    {
        RuleFor(x => x.PassengerId).NotEmpty().WithMessage("PassengerId is required");
        RuleFor(x => x.PickupLocation).NotEmpty().WithMessage("PickupLocation is required");
        RuleFor(x => x.Destination).NotEmpty().WithMessage("Destination is required");
    }
}