using Driver.Application.Drivers.Commands.CreateDriver;
using Driver.Application.Drivers.Commands.UpdateDriverStatus;
using Driver.Application.Drivers.Queries;
using Driver.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly IMediator _mediator;

    public DriverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get driver by ID
    /// </summary>
    [HttpGet("{driverId:guid}")]
    [ProducesResponseType(typeof(DriverResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DriverResponseDto>> GetDriverById(string driverId,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetDriverByIdQuery(Guid.Parse(driverId));
            var response = await _mediator.Send(query, cancellationToken);

            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Driver with {driverId} not found");
        }
    }

    [HttpPatch("{driverId:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> UpdateDriverStatus(string driverId, string status,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new UpdateDriverStatusCommand(Guid.Parse(driverId), status);
            await _mediator.Send(command, cancellationToken);

            return Ok("Driver status updated");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DriverResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DriverResponseDto>>> GetDrivers(CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllDriversQuery();
            var response = await _mediator.Send(query, cancellationToken);

            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    ///     Creates new driver
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreateDriver([FromBody] CreateDriverRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateDriverCommand(request.FirstName, request.LastName,
                request.Email, request.FareAmount);

            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetDriverById),
                new { id = response },
                response
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}