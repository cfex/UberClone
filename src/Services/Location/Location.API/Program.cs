using Location.API.Exceptions;
using Location.Application;
using Location.Infrastructure;
using Location.Infrastructure.Services.Grpc.Location;

namespace Location.API;

public class Program
{
    public static void Main(string[] args)
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
        builder.Services.AddLocationApplication(builder.Configuration);
        builder.Services.AddLocationInfrastructure(builder.Configuration);
        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.MapControllers();
        app.MapGrpcService<LocationGrpcService>();
        app.UseHttpsRedirection();

        app.Run();
    }
}