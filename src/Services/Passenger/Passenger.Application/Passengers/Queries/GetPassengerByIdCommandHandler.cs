using MediatR;
using Passenger.Application.Dto;
using Passenger.Domain.Repositories;

namespace Passenger.Application.Passengers.Queries;

public class GetPassengerByIdCommandHandler : IRequestHandler<GetPassengerByIdCommand, PassengerResponseDto>
{
    private readonly IPassengerRespository _respository;

    public GetPassengerByIdCommandHandler(IPassengerRespository respository)
    {
        _respository = respository;
    }

    public async Task<PassengerResponseDto> Handle(GetPassengerByIdCommand request, CancellationToken cancellationToken)
    {
        var passenger = await _respository.GetByIdAsync(request.PassengerId, cancellationToken);
        if (passenger == null) throw new Exception("Passenger not found");

        return PassengerResponseDto.Create(passenger.FullName, passenger.Email, passenger.Status);
    }
}