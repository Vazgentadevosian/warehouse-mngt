using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WRMNGT.Application.Locations.Interfaces;
using WRMNGT.Application.Locations.Utils;
using WRMNGT.Data.Entities;
using WRMNGT.Domain.Models.Request;
using WRMNGT.Domain.Models.Response;
using WRMNGT.Infrastructure.Abstractions.Repository;
using WRMNGT.Infrastructure.Helpers;

namespace WRMNGT.Application.Locations.Services;

public class LocationService : ILocationService
{
    private readonly IRepository<Location> _locationRepository;
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IMapper _mapper;

    public LocationService(IRepository<Location> locationRepository,
                           IRepository<Booking> bookingRepository,
                           IMapper mapper)
    {
        _locationRepository = locationRepository;
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<GetLocationResponseDto> GetLocation(GetLocationRequestDto dto, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetFirstAsync(q => q.Id == dto.Id, cancellationToken);

        return _mapper.Map<GetLocationResponseDto>(location);
    }

#warning for sure we need also send paginated result but not now
    public async Task<GetLocationsResponseDto> GetLocations(GetLocationsRequestDto dto, CancellationToken cancellationToken)
    {
        var locations = await _locationRepository.AsQueryable()
                                                 .AsNoTracking()
                                                 .Paginate(dto.Page, dto.PageSize)
                                                 .Include(l => l.Bookings)
                                                 .ToListAsync(cancellationToken);

        return new GetLocationsResponseDto()
        {
            Locations = _mapper.Map<GetLocationResponseDto[]>(locations)
        };
    }

    public async Task<CreateLocationResponseDto> CreateLocation(CreateLocationRequestDto dto, CancellationToken cancellationToken)
    {
        var location = _mapper.Map<Location>(dto);
        var locationCreated = await _locationRepository.AddAsync(location, cancellationToken);

        await _locationRepository.SaveAsync(cancellationToken);

        return _mapper.Map<CreateLocationResponseDto>(locationCreated);
    }

    public async Task<MakeBookingResponseDto> MakeBooking(MakeBookingRequestDto dto, CancellationToken cancellationToken)
    {
        var booking = _mapper.Map<Booking>(dto);

        var location = await _locationRepository.AsQueryable()
                                                .AsNoTracking()
                                                .Include(q => q.Bookings.Where(w => w.Date.Date == booking.Date.Date))
                                                .FirstAsync(q => q.Id == dto.LocationId);

        if (!BookingRulesValidator.IsBookingValid(location, booking))
            throw new ApplicationException("Booking is invalid");

        var bookingsCreated = await _bookingRepository.AddAsync(booking, cancellationToken);
        await _bookingRepository.SaveAsync(cancellationToken);

        return _mapper.Map<MakeBookingResponseDto>(bookingsCreated);
    }

    public async Task<ProcessBookingResponseDto> ProcessBooking(ProcessBookingRequestDto dto, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.AsQueryable()
                                              .FirstAsync(q => q.Id == dto.BookingId, cancellationToken);

        if (!BookingRulesValidator.BookingCanBeProcessed(booking))
            throw new ApplicationException("Booking is completed");

        booking.UpdateBookingState();

        await _bookingRepository.SaveAsync(cancellationToken);
        return _mapper.Map<ProcessBookingResponseDto>(booking);
    }
}