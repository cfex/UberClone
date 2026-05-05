using FluentValidation;
using Location.Application.Locations.Commands.UpdateLocation;

namespace Location.Application.Locations.Commands.UpdateDriverLocation;

public sealed class UpdateDriverLocationCommandValidator : AbstractValidator<UpdateDriverLocationCommand>
{
    public UpdateDriverLocationCommandValidator()
    {
        RuleFor(d => d.Latitude)
            .NotEmpty()
            .WithMessage("Latitude is required");
        RuleFor(d => d.Longitude)
            .NotEmpty()
            .WithMessage("Longitude is required");
    }
}