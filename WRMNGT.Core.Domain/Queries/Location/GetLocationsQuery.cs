using MediatR;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Domain.Queries.Location;

public class GetLocationsQuery : IRequest<GetLocationsResponseDto>
{
}