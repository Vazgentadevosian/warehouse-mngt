using AutoMapper;
using MediatR;
using WRMNGT.Application.Locations.Interfaces;
using WRMNGT.Domain.Commands.Location;
using WRMNGT.Domain.Models.Request;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Application.Handlers.Location.Commands;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, CreateLocationResponseDto>
{
    public IMapper Mapper { get; set; }
    public ILocationService LocationService { get; }

    public CreateLocationCommandHandler(IMapper mapper, ILocationService locationService)
    {
        Mapper = mapper;
        LocationService = locationService;
    }

    public Task<CreateLocationResponseDto> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var dto = Mapper.Map<CreateLocationRequestDto>(request);

        return LocationService.CreateLocation(dto, cancellationToken);
    }
}