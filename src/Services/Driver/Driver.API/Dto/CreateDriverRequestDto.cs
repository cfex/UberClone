namespace Driver.API.Dto;

public record CreateDriverRequestDto(string FirstName, string LastName, string Email, double FareAmount);