using AutoFixture;
using WRMNGT.Data.Entities;

namespace WRMNGT.Tests.Customizations
{
    public static class LocationCustomization
    {
        public static Location AddLocationCustomization(this IFixture fixture)
        {
            Location location = CreateLocation();
            fixture.Register(() => CreateLocation());

            return location;
        }

        private static Location CreateLocation()
        {
            // Create the mock objects
            return new Location
            {
                Address = "some address",
                Capacity = 4,
                OpenTime = TimeSpan.MinValue,
                CloseTime = TimeSpan.MinValue,
                Name = "some Name"
            };
        }
    }
}
