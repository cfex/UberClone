using FluentValidation;

namespace Driver.Application.Drivers.Commands.UpdateDriverStatus;

public sealed class UpdateDriverStatusCommandValidator : AbstractValidator<UpdateDriverStatusCommand>
{
    public UpdateDriverStatusCommandValidator()
    {
        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("Driver ID is required");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .IsInEnum().WithMessage("Invalid driver status");
    }
}
