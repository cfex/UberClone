using FluentValidation;

namespace Driver.Application.Drivers.Commands.CreateDriver;

public sealed class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
{
    public CreateDriverCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2, 100).WithMessage("First name must be between 2 and 100 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters");

        RuleFor(x => x.FareAmount)
            .GreaterThan(0).WithMessage("FareAmount must be greater than 0")
            .LessThan(1000).WithMessage("FareAmount must be less than 1000");
    }
}