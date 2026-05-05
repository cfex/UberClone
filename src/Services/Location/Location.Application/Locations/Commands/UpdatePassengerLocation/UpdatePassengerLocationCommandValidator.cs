using FluentValidation;

namespace Location.Application.Locations.Commands.UpdatePassengerLocation;

public sealed class UpdatePassengerLocationCommandValidator : AbstractValidator<UpdatePassengerLocationCommand>
{
    public UpdatePassengerLocationCommandValidator()
    {
        RuleFor(d => d.Latitude)
            .NotEmpty()
            .WithMessage("Latitude is required");
        RuleFor(d => d.Longitude)
            .NotEmpty()
            .WithMessage("Longitude is required");
    }
}