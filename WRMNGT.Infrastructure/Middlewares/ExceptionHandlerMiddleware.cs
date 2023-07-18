using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WRMNGT.Infrastructure.Error;
using WRMNGT.Infrastructure.Exceptions;
using WRMNGT.Infrastructure.Extensions.Common;
using WRMNGT.Infrastructure.Models.Response;
using WRMNGT.Infrastructure.Models.Response.Errors;

namespace WRMNGT.Infrastructure.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ResponseModel<object> { Message = exception.Message, };
            var code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    response.Errors = badRequestException.Errors;
                    break;
                case TaskCanceledException _:
                    code = HttpStatusCode.BadRequest;
                    response.Errors = new List<ErrorModel>
                    {
                        new ErrorModel(
                            ErrorCodes.RequestCancelled,
                            "Request was cancelled")
                    };
                    break;
                default:
                    _logger.LogError(exception.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            response.StatusCode = code;

            return context.Response.WriteAsync(response.ToJson());
        }
    }
}
