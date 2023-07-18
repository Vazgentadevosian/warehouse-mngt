using WRMNGT.Domain.Models.Request;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Application.Locations.Interfaces;

public interface ILocationService
{
    Task<GetLocationResponseDto> GetLocation(GetLocationRequestDto dto, CancellationToken cancellationToken = default);

    Task<GetLocationsResponseDto> GetLocations(GetLocationsRequestDto dto, CancellationToken cancellationToken = default);

    Task<CreateLocationResponseDto> CreateLocation(CreateLocationRequestDto dto, CancellationToken cancellationToken = default);

    Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto dto, CancellationToken cancellationToken = default);

    Task<ProcessBookingResponseDto> ProcessBooking(ProcessBookingRequestDto dto, CancellationToken cancellationToken = default);
}