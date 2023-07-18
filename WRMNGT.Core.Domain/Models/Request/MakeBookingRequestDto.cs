namespace WRMNGT.Domain.Models.Request;

public class MakeBookingRequestDto
{
    public Guid LocationId { get; set; }
    
    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public string Goods { get; set; }

    public string Carrier { get; set; }
}