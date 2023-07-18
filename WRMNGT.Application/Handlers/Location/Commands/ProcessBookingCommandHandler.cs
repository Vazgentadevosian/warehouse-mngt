using AutoMapper;
using MediatR;
using WRMNGT.Application.Locations.Interfaces;
using WRMNGT.Domain.Commands.Location;
using WRMNGT.Domain.Models.Request;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Application.Handlers.Location.Commands;

public class ProcessBookingCommandHandler : IRequestHandler<ProcessBookingCommand, ProcessBookingResponseDto>
{
    public IMapper Mapper { get; set; }
    public ILocationService LocationService { get; }

    public ProcessBookingCommandHandler(IMapper mapper, ILocationService locationService)
    {
        Mapper = mapper;
        LocationService = locationService;
    }

    public Task<ProcessBookingResponseDto> Handle(ProcessBookingCommand request, CancellationToken cancellationToken)
    {
        var dto = Mapper.Map<ProcessBookingRequestDto>(request);

        return LocationService.ProcessBooking(dto, cancellationToken);
    }
}