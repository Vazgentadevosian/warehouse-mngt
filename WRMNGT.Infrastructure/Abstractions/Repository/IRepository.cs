using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WRMNGT.Infrastructure.Abstractions.Repository
{
    public interface IRepository<TEntity> : IDisposable 
    {
        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

        int Count();

        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

        void Delete(TEntity entity);

        void DeleteById(Guid id);

        void Delete(IEnumerable<TEntity> entity);

        void DeleteByIds(IEnumerable<Guid> Ids);

        TEntity Find(Expression<Func<TEntity, bool>> match);

        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);

        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        TEntity Get(Guid id);

        Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<(int count, ICollection<TEntity> items)> GetPagedAsync(int pageNumber, int pageSize);

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetAsync(Guid id);

        void Save();

        Task<int> SaveAsync(CancellationToken cancellationToken = default);

        TEntity Update(TEntity entity, object key);

        Task<TEntity> UpdateAsync(TEntity entity, object key, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> GetAllElements<TKey>(Expression<Func<TEntity, TKey>> orderByExpression, Expression<Func<TEntity, bool>> filterExpression, bool ascending, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        IQueryable<TEntity> AsQueryable();
    }
}
