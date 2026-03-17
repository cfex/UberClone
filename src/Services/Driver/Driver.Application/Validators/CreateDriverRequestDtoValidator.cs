using Driver.Application.Dtos;
using FluentValidation;

namespace Driver.Application.Validators;

internal sealed class CreateDriverRequestDtoValidator : AbstractValidator<CreateDriverRequestDto>
{
    public CreateDriverRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
        RuleFor(y => y.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2, 100).WithMessage("First name must be between 2 and 100 characters");
        RuleFor(y => y.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters");
        RuleFor(x => x.FareAmount)
            .NotEmpty().WithMessage("FareAmount is required")
            .GreaterThan(0).WithMessage("FareAmount must be greater than 0")
            .LessThan(100).WithMessage("FareAmount must be less than 100");
    }
}