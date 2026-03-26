using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ride.API.Dtos;
using Ride.Application.Rides.Commands.RequestRide;

namespace Ride.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RidesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RidesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> RequestRide([FromBody] CreateRideRequestDto request)
    {
        var command = new RequestRideCommand(Guid.Parse(request.PassengerId), request.Destination);

        var response = await _mediator.Send(command);

        return Created("id", response);
    }
}