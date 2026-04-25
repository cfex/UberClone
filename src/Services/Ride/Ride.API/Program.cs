using Ride.API.Exceptions;
using Ride.Application;
using Ride.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddRideInfrastructure(builder.Configuration);
builder.Services.AddRideApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseExceptionHandler();

//app.UseHttpsRedirection();    
app.MapControllers();

app.Run();