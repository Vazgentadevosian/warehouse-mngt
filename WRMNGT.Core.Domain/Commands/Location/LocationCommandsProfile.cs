using AutoMapper;
using WRMNGT.Domain.Models.Request;
using WRMNGT.Domain.Queries.Location;

namespace WRMNGT.Domain.Commands.Location;

public class LocationCommandsProfile : Profile
{
    public LocationCommandsProfile()
    {
        CreateMap<MakeBookingCommand, MakeBookingRequestDto>()
                  .ForMember(dest => dest.Time, act => act.MapFrom(src => src.Time.TimeOfDay));

        CreateMap<ProcessBookingCommand, ProcessBookingRequestDto>();
        
        CreateMap<CreateLocationCommand, CreateLocationRequestDto>()
            .ForMember(dest => dest.OpenTime, act => act.MapFrom(src => src.OpenTime.TimeOfDay))
            .ForMember(dest => dest.CloseTime, act => act.MapFrom(src => src.CloseTime.TimeOfDay));
    }
}