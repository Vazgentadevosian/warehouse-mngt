using AutoMapper;
using WRMNGT.Data.Entities;
using WRMNGT.Domain.Models.Request;

namespace WRMNGT.Domain.Models.Response;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Booking, MakeBookingResponseDto>().ReverseMap();
        
        CreateMap<Booking, ProcessBookingResponseDto>().ReverseMap();

        CreateMap<Booking, MakeBookingRequestDto>().ReverseMap();

        CreateMap<Location, GetLocationResponseDto>().ReverseMap();
        
        CreateMap<Location, CreateLocationRequestDto>().ReverseMap();

        CreateMap<Location, CreateLocationResponseDto>().ReverseMap();
    }

    public override string ProfileName => GetType().Name;
}