using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using WRMNGT.Infrastructure.Abstractions;

namespace Exponent.ProjectPortal.Infrastructure.Services
{
    public class DbContextTransactionAdapter<TContext> : IDbContextTransactionAdapter 
                                                  where TContext: DbContext
    {
        private readonly TContext _dbContext;

        public DbContextTransactionAdapter(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _dbContext.Database.CreateExecutionStrategy();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }

        public async Task<int> CommitTransactionAsync(IDbContextTransaction transaction)
        {
            var writtenCount = await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return writtenCount;
        }

        public Task RollBackTransactionAsync(IDbContextTransaction transaction)
        {
            return transaction.RollbackAsync();
        }
    }
}
