using Driver.API.Exceptions;
using Driver.Application;
using Driver.Infrastructure;

namespace Driver.API;

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
        builder.Services.AddDriverInfrastructure(builder.Configuration);
        builder.Services.AddDriverApplication(builder.Configuration);
        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.UseExceptionHandler();
        app.MapControllers();
        app.UseHttpsRedirection();

        app.Run();
    }
}