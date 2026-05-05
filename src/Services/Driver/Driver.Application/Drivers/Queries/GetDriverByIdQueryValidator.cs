using FluentValidation;

namespace Driver.Application.Drivers.Queries;

public sealed class GetDriverByIdQueryValidator : AbstractValidator<GetDriverByIdQuery>
{
    public GetDriverByIdQueryValidator()
    {
        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("Driver ID is required");
    }
}
