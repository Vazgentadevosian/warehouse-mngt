using Microsoft.Extensions.Configuration;

namespace WRMNGT.Infrastructure.Extensions.Services
{
    public static class ConfigurationExtensions
    {
        #region Database

        public static string GetDBConnectionString(this IConfiguration configuration)
        {
            // Here needs to get the connection string from secret store like AWS Key Vault
            var connectionString = configuration.GetConnectionString("WRMNGTDB");
            return connectionString;
        }

        #endregion
    }
}
