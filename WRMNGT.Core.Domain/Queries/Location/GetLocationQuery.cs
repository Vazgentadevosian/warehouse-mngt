using MediatR;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Domain.Queries.Location;

public class GetLocationQuery : IRequest<GetLocationResponseDto>
{
    public Guid Id { get; set; }
}