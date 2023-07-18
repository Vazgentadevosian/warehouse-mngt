using AutoMapper;
using MediatR;
using WRMNGT.Application.Locations.Interfaces;
using WRMNGT.Domain.Models.Request;
using WRMNGT.Domain.Models.Response;
using WRMNGT.Domain.Queries.Location;

namespace WRMNGT.Application.Handlers.Location.Queries;
public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, GetLocationResponseDto>
{
    public IMapper Mapper { get; set; }

    public ILocationService LocationService { get; }

    public GetLocationQueryHandler(IMapper mapper, ILocationService locationService)
    {
        Mapper = mapper;
        LocationService = locationService;
    }

    public Task<GetLocationResponseDto> Handle(GetLocationQuery request, CancellationToken cancellationToken)
    {
        var dto = Mapper.Map<GetLocationRequestDto>(request);

        return LocationService.GetLocation(dto, cancellationToken);
    }
}