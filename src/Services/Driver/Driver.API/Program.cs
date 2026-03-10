using Driver.Application;
using Driver.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDriverInfrastructure(builder.Configuration);
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly); });

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.Run();