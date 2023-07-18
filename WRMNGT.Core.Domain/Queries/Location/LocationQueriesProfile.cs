using AutoMapper;
using WRMNGT.Domain.Models.Request;

namespace WRMNGT.Domain.Queries.Location;

public class LocationQueriesProfile : Profile
{
    public LocationQueriesProfile()
    {
        CreateMap<GetLocationQuery, GetLocationRequestDto>();
        CreateMap<GetLocationsQuery, GetLocationsRequestDto>();
    }
}