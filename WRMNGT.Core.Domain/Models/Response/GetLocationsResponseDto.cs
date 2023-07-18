namespace WRMNGT.Domain.Models.Response;

public class GetLocationsResponseDto
{
    public IEnumerable<GetLocationResponseDto> Locations { get; set; }
}