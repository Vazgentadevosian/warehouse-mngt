using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WRMNGT.Infrastructure.Abstractions;
using WRMNGT.Infrastructure.Extensions.Common;

namespace WRMNGT.Infrastructure.Middlewares
{
    public class DbContextTransactionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DbContextTransactionMiddleware> _logger;

        public DbContextTransactionMiddleware(
            RequestDelegate next,
            ILogger<DbContextTransactionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(
            HttpContext context,
            IDbContextTransactionAdapter transactionDbContext)
        {
            if (context.IsGetRequest() ||
                context.CheckHeaderForValue("ExplicitRequest", "true"))
            {
                await _next(context);
            }
            else
            {
                var strategy = transactionDbContext.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var transaction = await transactionDbContext.BeginTransactionAsync();

                    try
                    {
                        await _next(context);

                        if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
                        {
                            await transactionDbContext.RollBackTransactionAsync(transaction);
                        }
                        else
                        {
                            _logger.LogTrace($"Commiting transaction: {transaction.TransactionId}");
                            await transactionDbContext.CommitTransactionAsync(transaction);
                        }
                    }
                    catch
                    {
                        _logger.LogTrace($"Rolling back transaction: {transaction.TransactionId}");
                        await transactionDbContext.RollBackTransactionAsync(transaction);
                        throw;
                    }
                    finally
                    {
                        await transaction.DisposeAsync();
                    }
                });

            }
        }
    }
}
