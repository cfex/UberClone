using Driver.API.Grpc;
using Ride.API.Services;
using Ride.Application;
using Ride.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var driverGrpcAddress = builder.Configuration["GrpcServices:DriverService"]
                        ?? "http://localhost:5001";

builder.Services.AddGrpcClient<DriverService.DriverServiceClient>(options =>
{
    options.Address = new Uri(driverGrpcAddress);
});

builder.Services.AddScoped<IDriverGrpcClient, DriverGrpcClient>();

builder.Services.AddRideInfrastructure(builder.Configuration);
builder.Services.AddRideApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

//app.UseHttpsRedirection();    
app.MapControllers();

app.Run();