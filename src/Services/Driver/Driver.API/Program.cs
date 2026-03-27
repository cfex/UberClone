using System.Net;
using Driver.API.Exceptions;
using Driver.API.Grpc;
using Driver.Application;
using Driver.Infrastructure;
using Driver.Infrastructure.Persistence;
using Driver.Infrastructure.Seeding;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Driver.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
            options.Listen(IPAddress.Any, 5001,
                listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
        });
        builder.Services.AddGrpc();

        builder.Services.AddDriverInfrastructure(builder.Configuration);
        builder.Services.AddDriverApplication(builder.Configuration);
        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.UseExceptionHandler();

        app.MapGrpcService<DriverGrpcService>();


        app.MapControllers();
        app.UseHttpsRedirection();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DriverDbContext>();
            await DriverSeeder.SeedDriversAsync(context);
        }

        await app.RunAsync();
    }
}