using System.Net;

using WRMNGT.Infrastructure.Models.Response.Errors;

namespace WRMNGT.Infrastructure.Models.Response
{
    public class ResponseModel<T> : ResponseModel
    {
        public ResponseModel() { }

        public ResponseModel(T item) => Item = item;

        public T Item { get; set; }

        public static ResponseModel<T> Created(T item) => New(HttpStatusCode.Created, item: item);

        public new static ResponseModel<T> NotFound(
            string message = default,
            params ErrorModel[] errors) =>
            New(HttpStatusCode.NotFound, message, errors: errors);

        public static ResponseModel<T> Accepted(T item) => New(HttpStatusCode.Accepted, item: item);

        public static ResponseModel<T> Ok(T item) => New(HttpStatusCode.OK, item: item);

        public override object GetItem() => Item;

        public static Task<ResponseModel<T>> WithTask(
           HttpStatusCode statusCode = HttpStatusCode.OK,
           string message = default,
           T item = default,
           params ErrorModel[] errors)
           => Task.FromResult(New(statusCode, message, item, errors));

        public new static ResponseModel<T> SetError(
            HttpStatusCode statusCode = HttpStatusCode.BadRequest,
            string message = "Something went wrong",
            params ErrorModel[] errors) =>
            New(statusCode, message, errors: errors);

        private static ResponseModel<T> New(
            HttpStatusCode statusCode,
            string message = default,
            T item = default,
            params ErrorModel[] errors) =>
            new ResponseModel<T>(item)
            {
                StatusCode = statusCode,
                Message = message,
                Errors = errors?.ToList(),
            };
    }
}
