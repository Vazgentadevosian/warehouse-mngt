using System.Net;
using Microsoft.Extensions.Logging;
using WRMNGT.Infrastructure.Models.Response;
using WRMNGT.Infrastructure.Models.Response.Errors;

namespace WRMNGT.Infrastructure.Abstractions
{
    public abstract class HandlerBase<TBase>
    {
        protected readonly ILogger<TBase> _logger;

        protected HandlerBase(ILogger<TBase> logger)
        {
            _logger = logger;
        }

        protected ResponseModel<T> Ok<T>(T item) =>
            ResponseModel<T>.Ok(item);

        protected ResponseModel<T> Created<T>(T item) => 
            ResponseModel<T>.Created(item);

        protected ResponseModel NoContent() =>
            ResponseModel.NoContent();

        protected ResponseModel<T> BadRequest<T>(
            int code,
            string title) =>
            Error<T>(HttpStatusCode.BadRequest, code, title);

        protected ResponseModel BadRequest(
            int code,
            string title) =>
            Error(HttpStatusCode.BadRequest, code, title);

        protected ResponseModel<T> Unauthorized<T>(
            int code,
            string title) =>
            Error<T>(HttpStatusCode.Unauthorized, code, title);

        protected ResponseModel<T> Forbidden<T>(
            int code,
            string title) =>
            Error<T>(HttpStatusCode.Forbidden, code, title);

        protected ResponseModel<T> NotFound<T>(
            int code,
            string title) =>
            Error<T>(HttpStatusCode.NotFound, code, title);

        protected ResponseModel NotFound(
            int code,
            string title) =>
            Error(HttpStatusCode.NotFound, code, title);

        protected ResponseModel<T> Error<T>(
            HttpStatusCode statusCode,
            int code,
            string title) =>
            ResponseModel<T>.SetError(statusCode, errors: new ErrorModel(code, title));

        protected ResponseModel Error(
            HttpStatusCode statusCode,
            int code,
            string title) =>
            ResponseModel.SetError(statusCode, errors: new ErrorModel(code, title));
    }
}
