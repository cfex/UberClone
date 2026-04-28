using Driver.Infrastructure.Persistence;
using Driver.Infrastructure.Seeding;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class SeedController : ControllerBase
{
    private readonly DriverDbContext _context;
    private readonly ILogger<SeedController> _logger;

    public SeedController(DriverDbContext context, ILogger<SeedController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("drivers")]
    public async Task<IActionResult> SeedDrivers([FromQuery] int count = 100)
    {
        try
        {
            _logger.LogInformation("Starting to seed {Count} drivers", count);
            await DriverSeeder.SeedDriversAsync(_context, count);
            return Ok(new { message = $"Successfully seeded {count} drivers" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding drivers");
            return StatusCode(500, new { error = ex.Message });
        }
    }
}