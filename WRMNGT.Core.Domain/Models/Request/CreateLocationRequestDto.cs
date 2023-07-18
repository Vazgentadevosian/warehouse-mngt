namespace WRMNGT.Domain.Models.Request;
public class CreateLocationRequestDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
}