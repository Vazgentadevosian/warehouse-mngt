using Exponent.ProjectPortal.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WRMNGT.Infrastructure.Abstractions;

namespace WRMNGT.Infrastructure.Extensions.Services
{
    public static class ServiceCollectionExtensions
    {
        #region Database

        public static IServiceCollection AddEFCore<TContext>(this IServiceCollection services,
                                                                   string connectionString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
                  options.UseSqlServer(connectionString,
                      sqlServerOptionsAction: sqlOptions =>
                      {
                          sqlOptions.EnableRetryOnFailure();
                      })
              );
            return services.AddScoped<DbContext, TContext>();
        }

        #endregion

        #region General

        public static IServiceCollection AddServices<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IDbContextTransactionAdapter, DbContextTransactionAdapter<TContext>>();

            return services;
        }

        #endregion
    }
}
