namespace WRMNGT.Domain.Models.Response;

public class GetLocationResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
}