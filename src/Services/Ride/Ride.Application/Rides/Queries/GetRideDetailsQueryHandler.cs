using MediatR;
using Ride.Application.Dto;
using Ride.Domain.Repositories;

namespace Ride.Application.Rides.Queries;

public class GetRideDetailsQueryHandler : IRequestHandler<GetRideDetailsQuery, RideDetailsDto>
{
    private readonly IRideRepository _repository;

    public GetRideDetailsQueryHandler(IRideRepository repository)
    {
        _repository = repository;
    }

    public async Task<RideDetailsDto> Handle(GetRideDetailsQuery request, CancellationToken cancellationToken)
    {
        var ride = await _repository.GetByIdAsync(request.RideId, cancellationToken);
        if (ride == null) throw new Exception("Ride not found");

        return RideDetailsDto.Create(ride.Id, ride.DriverId, ride.PassengerId, ride.CreatedAt);
    }
}