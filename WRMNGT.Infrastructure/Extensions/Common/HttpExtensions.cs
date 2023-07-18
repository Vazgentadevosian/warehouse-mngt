using System.Net;
using Microsoft.AspNetCore.Http;

namespace WRMNGT.Infrastructure.Extensions.Common
{
    public static class HttpExtensions
    {
        private const string GET = "GET";
#pragma warning disable IDE0051 // Remove unused private members
        private const string POST = "POST";
        private const string PUT = "PUT";
        private const string DELETE = "DELETE";
#pragma warning restore IDE0051 // Remove unused private members

        public static bool IsGetRequest(this HttpContext context)
        {
            return context.Request.Method == GET;
        }

        public static string GetClaimValue(this IHttpContextAccessor httpContextAccessor, string key)
        {
            return httpContextAccessor.HttpContext.User.GetClaimValue(key);
        }

        public static bool CheckHeaderForValue(this HttpContext context, string key, string value)
        {
            var header = context.Request.Headers[key];
            return header == value;
        }

        public static bool IsSuccessStatusCode(this HttpStatusCode statusCode)
            => ((int)statusCode).IsSuccessStatusCode();

        public static bool IsSuccessStatusCode(this int statusCode) =>
            statusCode >= 200 && statusCode <= 299;
    }
}
