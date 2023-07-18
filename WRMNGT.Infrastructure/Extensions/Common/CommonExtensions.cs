using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

namespace WRMNGT.Infrastructure.Extensions.Common
{
    public static class CommonExtensions
    {
        public static string ToJson(this object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            return JsonConvert.SerializeObject(obj, Formatting.None, settings);
        }

        public static IEnumerable<Claim> GetClaims(this ClaimsPrincipal principal, string type)
        {
            return principal.Claims.Where(q => q.Type == type);
        }

        public static string GetClaimValue(this ClaimsPrincipal principal, string type)
        {
            return principal.Claims.SingleOrDefault(q => q.Type == type)?.Value;
        }

        public static byte[] GetBytes(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static string Join(this IEnumerable<string> strings, char sep = ',')
        {
            return string.Join(sep, strings);
        }

        public static bool IsDigitsOnly(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static void ThrowIfCancelled(this CancellationToken cToken) =>
            cToken.ThrowIfCancellationRequested();
    }
}
