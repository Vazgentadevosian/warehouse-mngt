using WRMNGT.Infrastructure.Models.Response.Errors;

namespace WRMNGT.Infrastructure.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<ErrorModel> Errors { get; set; }

        public BadRequestException(
            string message,
            List<ErrorModel> errors = default) : base(message)
        {
            Errors = errors;
        }
    }
}
