using Driver.Application.Dtos;

namespace Driver.Application.Extensions;

public static class DriverMappingExtensions
{
    public static DriverResponseDto ToDto(this Domain.Entities.Driver driver)
    {
        return DriverResponseDto.Create(
            driver.Id,
            driver.FullName.FirstName,
            driver.Email.Value,
            driver.Status.ToString(),
            driver.Fare
        );
    }

    public static List<DriverResponseDto> ToDtoList(this IEnumerable<Domain.Entities.Driver> drivers)
    {
        return drivers.Select(d => d.ToDto()).ToList();
    }
}