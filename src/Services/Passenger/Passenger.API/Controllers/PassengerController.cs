using MediatR;
using Microsoft.AspNetCore.Mvc;
using Passenger.Application.Dto;
using Passenger.Application.Passengers.Queries;

namespace Passenger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassengerController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public PassengerController(ILogger logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("{passengerId:guid}")]
    [ProducesResponseType(typeof(PassengerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PassengerResponseDto>> GetPassengerById([FromRoute] Guid passengerId)
    {
        var query = new GetPassengerByIdCommand(passengerId);
        var passenger = await _mediator.Send(query);

        return Ok(passenger);
    }
}