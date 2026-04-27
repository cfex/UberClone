using Bogus;
using Driver.Domain.Entities;
using Driver.Domain.Enums;
using Driver.Domain.ValueObjects;
using Driver.Infrastructure.Persistence;

namespace Driver.Infrastructure.Seeding;

public static class DriverSeeder
{
    public static async Task SeedDriversAsync(DriverDbContext context, int count = 100)
    {
        if (context.Drivers.Any())
        {
            Console.WriteLine("Drivers already exist in database. Skipping seed.");
            return;
        }

        var vehicleFaker = new Faker<Vehicle>()
            .CustomInstantiator(f => Vehicle.Create(
                f.Vehicle.Manufacturer(),
                f.Vehicle.Model(),
                f.Vehicle.Vin(),
                f.Commerce.Color(),
                f.Date.Between(DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow.AddYears(1)).ToUniversalTime()
            ));

        var documentFaker = new Faker<Document>()
            .CustomInstantiator(f => Document.Create(
                Guid.NewGuid(),
                f.PickRandom<DocumentType>(),
                f.Date.Future(2).ToUniversalTime()
            ));

        var activeStatuses = new[]
        {
            DriverStatus.Offline,
            DriverStatus.Online,
            DriverStatus.Available,
            DriverStatus.OnRide,
            DriverStatus.Commuting
        };

        var driverFaker = new Faker<Domain.Entities.Driver>()
            .CustomInstantiator(f =>
            {
                var firstName = f.Name.FirstName();
                var lastName = f.Name.LastName();
                var email = f.Internet.Email(firstName, lastName).ToLower();

                return Domain.Entities.Driver.Create(
                    Guid.NewGuid(),
                    FullName.Create(firstName, lastName),
                    Email.Create(email, true),
                    f.PickRandom(activeStatuses),
                    Money.Create(f.Random.Double(5, 1000.0), f.PickRandom<Currency>()),
                    "driver",
                    documentFaker.Generate(),
                    vehicleFaker.Generate()
                );
            });

        var drivers = driverFaker.Generate(count);

        await context.Drivers.AddRangeAsync(drivers);
        await context.SaveChangesAsync();

        Console.WriteLine($"✅ Seeded {count} drivers successfully!");
    }
}