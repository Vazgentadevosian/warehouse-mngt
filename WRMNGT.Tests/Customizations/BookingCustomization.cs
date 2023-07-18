using AutoFixture;
using WRMNGT.Data.Entities;

namespace WRMNGT.Tests.Customizations
{
    public static class BookingCustomization
    {
        public static Booking AddBookingCustomization(this IFixture fixture)
        {
            Booking booking = CreateBooking();
            fixture.Register(() => CreateBooking());

            return booking;
        }

        private static Booking CreateBooking()
        {
            // Create the mock objects
            return new Booking
            {
                Date = DateTime.UtcNow,
                Time = TimeSpan.MinValue,
                Goods = "some goods",
                Carrier = "some carrier",
                State = BookingState.LOADING,
                Location = new Location
                {
                    Address = "some address",
                    Capacity = 4,
                    OpenTime = TimeSpan.MinValue,
                    CloseTime = TimeSpan.MinValue,
                    Name = "some Name"
                }
            };
        }
    }
}
