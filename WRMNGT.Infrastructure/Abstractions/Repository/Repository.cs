using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using WRMNGT.Data.Database;
using WRMNGT.Infrastructure.Extensions.Common;
using WRMNGT.Infrastructure.Models.Entities;

namespace WRMNGT.Infrastructure.Abstractions.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly WrMngtContext Context;

        public Repository(WrMngtContext context)
        {
            Context = context;
        }

        public async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Set().ToListAsync(cancellationToken);
        }

        public async Task<(int count, ICollection<TEntity> items)> GetPagedAsync(int pageNumber, int pageSize)
        {

            var skip = (pageNumber - 1) * pageSize;
            var items = await Set()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var count = await Set().CountAsync();

            return (count, items);
        }

        public TEntity Get(Guid id)
        {
            return Set().Find(id);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await Set().FindAsync(id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            Set().Add(entity);
            return entity;
        }

        public async virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken token)
        {
            await Set().AddAsync(entity, token);
            return entity;
        }

        /// <summary>
        /// Adds models in db (don't returns correct ids, for that case get fresh ids from storage) 
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async virtual Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken token)
        {
            await Set().AddRangeAsync(entities);
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token)
        {
            return Set().AnyAsync(filter, token);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return Set().SingleOrDefault(match);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken)
        {
            return await Set().SingleOrDefaultAsync(match, cancellationToken);
        }

        public async Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Set().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        {
            var set = AsQueryable();
            if (includes.Any())
            {
                set = includes.Aggregate(set,
                    (current, include) => current.Include(include));
            }
            return await set.FirstOrDefaultAsync(ent => ent.Id == id, cancellationToken);
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return Set().Where(match).ToList();
        }

        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken)
        {
            return await Set().Where(match).ToListAsync(cancellationToken);
        }

        public virtual void Delete(TEntity entity)
        {
            Set().Remove(entity);
        }

        public void DeleteById(Guid id)
        {
            TEntity exist = Set().Where(e => e.Id == id).FirstOrDefault();
            if (exist == null)
                throw new ArgumentException($"Entity with Id={id} not found to delete", nameof(id));

            Set().Remove(exist);
        }

        public virtual void Delete(IEnumerable<TEntity> entity)
        {
            Set().RemoveRange(entity);
        }

        public virtual void DeleteByIds(IEnumerable<Guid> ids)
        {
            IEnumerable<TEntity> items = Set().Where(e => ids.Contains(e.Id));
            if (items.IsEmpty() || items == null)
                throw new ArgumentException($"Entities with given Ids not found to delete", nameof(ids));

            Set().RemoveRange(items);
        }

        public TEntity Update(TEntity entity, object key)
        {
            TEntity exist = Set().Find(key);
            if (exist is null)
                throw new ArgumentException($"Entity with key={key} not found to update", nameof(key));

            UpdateFactoryMethod(entity, exist);

            Context.Entry(exist).CurrentValues.SetValues(entity);

            return exist;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, object key, CancellationToken token)
        {
            TEntity exist = await Set().FindAsync(key);
            if (exist is null)
                throw new ArgumentException($"Entity with key={key} not found to update", nameof(key));

            UpdateFactoryMethod(entity, exist);
            
            Context.Entry(exist).CurrentValues.SetValues(entity);
            return exist;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token)
        {
            return await UpdateAsync(entity, entity.Id, token);
        }

        protected virtual void UpdateFactoryMethod(TEntity entity, TEntity existingModel) { }

        public int Count()
        {
            return Set().Count();
        }

        public async Task<int> CountAsync(CancellationToken token)
        {
            return await Set().CountAsync(token);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token)
        {
            var entitySet = Set().AsNoTracking();
            return entitySet.CountAsync(filter, token);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = Set().Where(predicate);
            return query;
        }

        public async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Set().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> queryable = Set();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }
            return queryable;
        }

        public IEnumerable<TEntity> GetAllElements<TKey>(Expression<Func<TEntity, TKey>> orderByExpression,
                                                         Expression<Func<TEntity, bool>> filterExpression,
                                                         bool ascending,
                                                  params Expression<Func<TEntity, object>>[] includes)
        {
            var entitySet = Set().AsNoTracking();

            var searchedSet = filterExpression == null ? entitySet : entitySet.Where(filterExpression);

            if (ascending)
            {
                if (includes.Any())
                {
                    return includes.Aggregate(searchedSet.OrderBy(orderByExpression).AsQueryable(),
                        (current, include) => current.Include(include));
                }

                return searchedSet.OrderBy(orderByExpression);
            }

            if (includes.Any())
            {
                return includes.Aggregate(searchedSet.OrderBy(orderByExpression).AsQueryable(),
                    (current, include) => current.Include(include));
            }

            return searchedSet.OrderByDescending(orderByExpression);
        }

        public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
                                                    CancellationToken cancellationToken)
        {
            var queryable = Set().AsNoTracking();

            var entity = filter == null
                ? queryable.FirstOrDefaultAsync(cancellationToken).Result
                : queryable.FirstOrDefaultAsync(filter, cancellationToken).Result;

            return Task.FromResult(entity);
        }

        public Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter,
                                                  CancellationToken cancellationToken)
        {
            var queryable = Set().AsNoTracking();

            var entity = filter == null
                ? queryable.FirstAsync(cancellationToken).Result
                : queryable.FirstAsync(filter, cancellationToken).Result;

            return Task.FromResult(entity);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return Set().AsQueryable();
        }

        private DbSet<TEntity> Set()
        {
            return Context.Set<TEntity>();
        }

        private bool _disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
