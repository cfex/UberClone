using Driver.Domain.Enums;
using MediatR;

namespace Driver.Application.Drivers.Commands.CreateDriver;

public sealed record CreateDriverCommand(
    string FirstName,
    string LastName,
    string Email,
    double FareAmount,
    Currency Currency,
    DocumentType DocumentType,
    DateTime DocumentExpiryDate,
    string VehicleMake,
    string VehicleModel,
    string VehicleLicensePlate,
    string VehicleColor,
    DateTime VehicleRegistrationDate
) : IRequest<Guid>;