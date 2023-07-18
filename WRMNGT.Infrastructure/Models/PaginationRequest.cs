using FluentValidation;

namespace WRMNGT.Infrastructure.Models
{
    /// <summary>
    /// Pagination model (Inherit from this if there will be any new params)
    /// </summary>
    public class PaginationRequest
    {
        public int PageSize { get; set; } = 50;

        public int Page { get; set; } = 1;
    }

    public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(m => m.PageSize).NotNull().GreaterThan(0);
            RuleFor(m => m.Page).NotNull().GreaterThan(0);
        }
    }
}
