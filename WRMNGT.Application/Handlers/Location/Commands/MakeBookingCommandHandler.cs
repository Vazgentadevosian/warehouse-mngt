using AutoMapper;
using MediatR;
using WRMNGT.Application.Locations.Interfaces;
using WRMNGT.Domain.Commands.Location;
using WRMNGT.Domain.Models.Request;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Application.Handlers.Location.Commands;

public class MakeBookingCommandHandler : IRequestHandler<MakeBookingCommand, MakeBookingResponseDto>
{
    public IMapper Mapper { get; set; }

    public ILocationService LocationService { get; }

    public MakeBookingCommandHandler(IMapper mapper, ILocationService locationService)
    {
        Mapper = mapper;
        LocationService = locationService;
    }

    public Task<MakeBookingResponseDto> Handle(MakeBookingCommand request, CancellationToken cancellationToken)
    {
        var dto = Mapper.Map<MakeBookingRequestDto>(request);

        return LocationService.MakeBooking(dto, cancellationToken);
    }
}