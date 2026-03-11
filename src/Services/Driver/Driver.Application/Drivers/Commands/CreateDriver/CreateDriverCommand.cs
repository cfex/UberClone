using Driver.Domain.Enums;
using MediatR;

namespace Driver.Application.Drivers.Commands.CreateDriver;

public sealed record CreateDriverCommand(
    string FirstName,
    string LastName,
    string Email,
    double FareAmount,
    DocumentType DocumentType = DocumentType.ID,
    DateTime DocumentExpiryDate = default,
    string VehicleMake = "",
    string VehicleModel = "",
    string VehicleLicensePlate = "",
    string VehicleColor = "",
    DateTime VehicleRegistrationDate = default,
    Currency Currency = Currency.EUR) : IRequest<Guid>;