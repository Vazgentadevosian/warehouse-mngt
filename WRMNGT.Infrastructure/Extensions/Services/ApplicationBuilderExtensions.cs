using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using WRMNGT.Infrastructure.Middlewares;

namespace WRMNGT.Infrastructure.Extensions.Services
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public static IApplicationBuilder UseDbTransactionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DbContextTransactionMiddleware>();
        }

    }
}
