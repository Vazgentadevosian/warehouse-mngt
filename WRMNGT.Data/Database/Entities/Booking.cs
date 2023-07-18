using System.ComponentModel.DataAnnotations.Schema;
using WRMNGT.Data.Database;
using WRMNGT.Infrastructure.Models.Entities;

namespace WRMNGT.Data.Entities;

public enum BookingState
{
    ARRIVED,
    LOADING,
    UNLOADING,
    COMPLETED
}

public class Booking : BaseEntity, IEntityCreatedAt
{
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Goods { get; set; }
    public string Carrier { get; set; }
    public BookingState State { get; set; }
    
    public Guid LocationId{ get; set; }
    public Location Location { get; set; }

    public DateTimeOffset CreatedAt { get; set; }


}
