using FluentValidation;
using MediatR;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Domain.Commands.Location;

public class ProcessBookingCommand : IRequest<ProcessBookingResponseDto>
{
    public Guid LocationId { get; set; }
    public Guid BookingId { get; set; }
    
    public class ProcessBookingCommandValidator : AbstractValidator<ProcessBookingCommand>
    {
        public ProcessBookingCommandValidator()
        {
            RuleFor(c => c.LocationId).NotEmpty();
            RuleFor(c => c.BookingId).NotEmpty();
        }
    }
}