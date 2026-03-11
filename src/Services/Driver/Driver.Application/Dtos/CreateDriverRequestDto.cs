namespace Driver.Application.Dtos;

public record CreateDriverRequestDto(string FirstName, string LastName, string Email, double FareAmount);