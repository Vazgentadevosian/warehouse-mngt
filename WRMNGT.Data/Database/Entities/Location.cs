using WRMNGT.Infrastructure.Models.Entities;

namespace WRMNGT.Data.Entities;

public class Location : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }

    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }

    public ICollection<Booking> Bookings { get; }
}
