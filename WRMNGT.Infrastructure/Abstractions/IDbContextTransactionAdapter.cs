using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace WRMNGT.Infrastructure.Abstractions
{
    public interface IDbContextTransactionAdapter
    {
        IExecutionStrategy CreateExecutionStrategy();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> CommitTransactionAsync(IDbContextTransaction transaction);
        Task RollBackTransactionAsync(IDbContextTransaction transaction);
    }
}
