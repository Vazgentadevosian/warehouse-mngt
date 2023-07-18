using System.Net;
using System.Text.Json.Serialization;
using WRMNGT.Infrastructure.Extensions.Common;
using WRMNGT.Infrastructure.Models.Response.Errors;

namespace WRMNGT.Infrastructure.Models.Response
{
    public abstract class ResponseModel
    {
        public string Message { get; set; }

        public List<ErrorModel> Errors { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        [JsonIgnore]
        public int StatusCodeValue => (int)StatusCode;

        [JsonIgnore]
        public bool IsSuccessStatusCode => StatusCodeValue.IsSuccessStatusCode();

        public abstract object GetItem();

        public static ResponseModel SetError(
            HttpStatusCode statusCode = HttpStatusCode.BadRequest,
            string message = "Something went wrong",
            params ErrorModel[] errors) =>
            New(statusCode, message, errors);

        public static ResponseModel NoContent() => New(HttpStatusCode.NoContent);

        public static ResponseModel Created() => New(HttpStatusCode.Created);

        public static ResponseModel NotFound(
            string message = default,
            params ErrorModel[] errors) =>
            New(HttpStatusCode.NotFound, message, errors);

        private static ResponseModel New(
            HttpStatusCode statusCode,
            string message = default,
            params ErrorModel[] errors) =>
            new ResponseModel<object>
            {
                StatusCode = statusCode,
                Message = message,
                Errors = errors.ToList(),
            };
    }
}
