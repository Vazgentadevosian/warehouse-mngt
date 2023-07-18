using FluentValidation;
using MediatR;
using WRMNGT.Domain.Models.Response;

namespace WRMNGT.Domain.Commands.Location;

public class CreateLocationCommand : IRequest<CreateLocationResponseDto>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public DateTimeOffset OpenTime { get; set; }
    public DateTimeOffset CloseTime { get; set; }


    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.OpenTime).NotEmpty();
            RuleFor(c => c.CloseTime).GreaterThan(x => x.OpenTime)
            .WithMessage("CloseTime must be greater than OpenTime.");
            RuleFor(c => c.Capacity).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
        }
    }
}