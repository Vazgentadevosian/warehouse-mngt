namespace WRMNGT.Application.Locations.Utils;

public class BookingTimeWindow
{
    public TimeSpan Start { get; private set; }
    public TimeSpan End { get; private set; }
    private List<TimeSlot> BookedSlots { get; set; }

    public BookingTimeWindow(TimeSpan start, TimeSpan end)
    {
        Start = start;
        End = end;

        BookedSlots = new List<TimeSlot>();
    }
    
    public bool CanBook(TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime >= Start && endTime <= End)
        {
            foreach (var bookedSlot in BookedSlots)
            {
                if (startTime < bookedSlot.EndTime && endTime > bookedSlot.StartTime)
                {
                    return false; // Conflict: overlapping time slot
                }
            }

            return true; // No conflicts
        }

        return false; // Outside of working hour window
    }

    public void BookTime(TimeSpan startTime, TimeSpan endTime)
    {
        if (CanBook(startTime, endTime))
        {
            BookedSlots.Add(new TimeSlot(startTime, endTime));
        }
        else
        {
            throw new ApplicationException("Booking cannot be made due to conflicts or outside of working hour window.");
        }
    }

    private class TimeSlot
    {
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }

        public TimeSlot(TimeSpan startTime, TimeSpan endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}