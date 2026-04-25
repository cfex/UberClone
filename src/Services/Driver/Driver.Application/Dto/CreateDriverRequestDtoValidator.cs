using FluentValidation;

namespace Driver.Application.Dtos;

public sealed class CreateDriverRequestDtoValidator : AbstractValidator<CreateDriverRequestDto>
{
    public CreateDriverRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters");

        RuleFor(x => x.FareAmount)
            .GreaterThan(0).WithMessage("Fare amount must be greater than 0");
    }
}
