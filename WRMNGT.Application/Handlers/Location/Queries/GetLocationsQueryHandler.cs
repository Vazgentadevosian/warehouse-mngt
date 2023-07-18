using AutoMapper;
using MediatR;
using WRMNGT.Application.Locations.Interfaces;
using WRMNGT.Domain.Models.Request;
using WRMNGT.Domain.Models.Response;
using WRMNGT.Domain.Queries.Location;

namespace WRMNGT.Application.Handlers.Location.Queries;
public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, GetLocationsResponseDto>
{
    public IMapper Mapper { get; set; }

    public ILocationService LocationService { get; }

    public GetLocationsQueryHandler(IMapper mapper, ILocationService locationService)
    {
        Mapper = mapper;
        LocationService = locationService;
    }

    public Task<GetLocationsResponseDto> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        var dto = Mapper.Map<GetLocationsRequestDto>(request);

        return LocationService.GetLocations(dto, cancellationToken);
    }
}