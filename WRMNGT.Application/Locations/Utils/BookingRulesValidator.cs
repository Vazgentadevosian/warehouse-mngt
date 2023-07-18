using WRMNGT.Data.Entities;

namespace WRMNGT.Application.Locations.Utils;

public static class BookingRulesValidator
{
    public static bool IsBookingValid(Location? l, Booking? b)
    {
        if (l == null || b == null)
        {
            return false;
        }

        return HasValidCapacity(l) && AvoidsConflictsWithOtherBookings(l, b);
    }

    public static bool HasValidStateTransition(Booking b, BookingState newState)
    {
        return (newState - b.State) == 1;
    }

    public static bool BookingCanBeCompleted(Booking b)
    {
        return b.State == BookingState.UNLOADING;
    }

    public static bool BookingCanBeCanceled(Booking b)
    {
        return b.State == BookingState.ARRIVED || b.State == BookingState.LOADING;
    }

    public static bool BookingCanBeProcessed(Booking b)
    {
        return b.State != BookingState.COMPLETED;
    }

    private static bool HasValidCapacity(Location l)
    {
        return l.Bookings.Count(q => q.State != BookingState.COMPLETED) < l.Capacity;
    }

    private static bool AvoidsConflictsWithOtherBookings(Location l, Booking b)
    {
        var btw = ConstructTimeWindow(l, l.Bookings.Where(dtItem => dtItem.Date.Date == b.Date.Date));

        return btw.CanBook(DateTimeToTimeSpan(b.Date), b.Time);
    }

    private static BookingTimeWindow ConstructTimeWindow(Location l, IEnumerable<Booking> bookings)
    {
        var btw = new BookingTimeWindow(l.OpenTime, l.CloseTime);

        foreach (var booking in bookings)
            btw.BookTime(DateTimeToTimeSpan(booking.Date), booking.Time);

        return btw;
    }

    private static TimeSpan DateTimeToTimeSpan(DateTime dt)
    {
        return new TimeSpan(dt.Hour, dt.Minute, dt.Second);
    }
}