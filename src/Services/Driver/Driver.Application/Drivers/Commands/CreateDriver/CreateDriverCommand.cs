using Driver.Domain.Enums;
using MediatR;

namespace Driver.Application.Drivers.Commands.CreateDriver;

public sealed record CreateDriverCommand(
    string FirstName,
    string LastName,
    string Email,
    double FareAmount,
    Currency Currency = Currency.EUR) : IRequest<Guid>;