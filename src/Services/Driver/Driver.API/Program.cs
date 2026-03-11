using Driver.Application;
using Driver.Infrastructure;

namespace Driver.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddDriverInfrastructure(builder.Configuration);
        builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly); });
        builder.Services.AddControllers();


        var app = builder.Build();

        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.MapControllers();
        app.UseHttpsRedirection();

        app.Run();
    }
}