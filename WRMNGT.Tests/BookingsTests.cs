using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using WRMNGT.Application.Locations.Interfaces;
using WRMNGT.Application.Locations.Services;
using WRMNGT.Data.Database;
using WRMNGT.Data.Entities;
using WRMNGT.Domain.Models.Request;
using WRMNGT.Infrastructure.Abstractions.Repository;
using WRMNGT.Tests.Attributes;
using WRMNGT.Tests.DB;

namespace WRMNGT.Tests
{
    public class BookingsTests
    {
        [Theory, AutoMoqData(true)]
        public void Success_GetLocations(Location location, Booking booking, [Frozen] Mock<IRepository<Location>> locationRepository, [Frozen] Mock<IRepository<Booking>> bookingRepository, [Frozen] IMapper _mapper)
        {
            using (var context = new WrMngtContext(InMemoryDatabaseOptions.CreateNewContextOptions<WrMngtContext>()))
            {
                // Create the mock objects
                context.Locations.Add(location);

                context.Bookings.Add(booking);

                context.SaveChanges();

                GetLocationRequestDto locationsRequestDto = new GetLocationRequestDto();
                locationsRequestDto.Id = location.Id;

                ILocationService locationService = new LocationService(new Repository<Location>(context), new Repository<Booking>(context), _mapper);


                var result = locationService.GetLocation(locationsRequestDto).GetAwaiter().GetResult();

                Assert.NotNull(result);
            }
        }
    }
}
