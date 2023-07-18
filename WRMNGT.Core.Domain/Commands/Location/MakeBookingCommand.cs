using FluentValidation;
using MediatR;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Domain.Commands.Location;

public class MakeBookingCommand : IRequest<MakeBookingResponseDto>
{
    public Guid LocationId { get; set; }
    public DateTime Date { get; set; }
    public DateTimeOffset Time { get; set; }
    public string Goods { get; set; }
    public string Carrier { get; set; }
    
    public class MakeBookingCommandValidator : AbstractValidator<MakeBookingCommand>
    {
        public MakeBookingCommandValidator()
        {
            RuleFor(c => c.LocationId).NotEmpty();
            RuleFor(c => c.Date).NotEmpty();
            RuleFor(c => c.Time).NotEmpty();
            RuleFor(c => c.Goods).NotEmpty();
            RuleFor(c => c.Carrier).NotEmpty();
        }
    }
}