using System.Net;
using Driver.API.Exceptions;
using Driver.Application;
using Driver.Infrastructure;
using Driver.Infrastructure.Configuration;
using Driver.Infrastructure.Persistence;
using Driver.Infrastructure.Seeding;
using Driver.Infrastructure.Services.gRPC.Driver;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Driver.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var driverGrpcOptions = builder.Configuration.GetSection(DriverGrpcOptions.SectionName).Get<DriverGrpcOptions>()
                                ?? throw new InvalidOperationException(
                                    "DriverGrpc configuration is missing or invalid.");

        builder.Services.AddProblemDetails(configure =>
        {
            configure.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
            };
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddOpenApi();

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, 5000,
                listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });
            options.Listen(IPAddress.Any, driverGrpcOptions.Port,
                listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
        });

        builder.Services.AddDriverInfrastructure(builder.Configuration);
        builder.Services.AddDriverApplication(builder.Configuration);
        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.UseExceptionHandler();

        app.MapGrpcService<DriverGrpcService>();


        app.MapControllers();
        app.UseHttpsRedirection();

        await app.StartAsync();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DriverDbContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            await DriverSeeder.SeedDriversAsync(context);
        }

        await app.WaitForShutdownAsync();
    }
}