using Driver.Application.Drivers.Commands.CreateDriver;
using Driver.Application.Drivers.Commands.UpdateDriverStatus;
using Driver.Application.Drivers.Queries;
using Driver.Application.Dto;
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
        var query = new GetDriverByIdQuery(Guid.Parse(driverId));
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPatch("{driverId:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> UpdateDriverStatus(string driverId, string status,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDriverStatusCommand(Guid.Parse(driverId), status);
        await _mediator.Send(command, cancellationToken);

        return Ok("Driver status updated");
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DriverResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DriverResponseDto>>> GetDrivers(CancellationToken cancellationToken)
    {
        var query = new GetAllDriversQuery();
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
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
        var command = new CreateDriverCommand(request.FirstName, request.LastName,
            request.Email, request.FareAmount);

        var response = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetDriverById),
            new { driverId = response },
            response
        );
    }
}