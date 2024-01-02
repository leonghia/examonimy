using ExamonimyWeb.DatabaseContexts;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Extensions;
using ExamonimyWeb.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamonimyWeb.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ExamonimyContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ExamonimyContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filterPredicate = null)
        {
            if (filterPredicate is not null)
                return await _dbSet.CountAsync(filterPredicate);
            else
                return await _dbSet.CountAsync();
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filterPredicate, List<string>? includedProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;
            query = query.Where(filterPredicate);

            if (includedProperties is not null)
            {
                foreach (var includedProperty in includedProperties)
                {
                    query = query.Include(includedProperty);
                }
            }

            return await query.FirstOrDefaultAsync();

        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<PagedList<TEntity>> GetPagedListAsync(RequestParams? requestParams, Expression<Func<TEntity, bool>>? predicate = null, List<string>? includedProps = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            requestParams ??= new RequestParams();

            IQueryable<TEntity> query = _dbSet;

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (includedProps is not null)
            {
                foreach (var includedProperty in includedProps)
                {
                    query = query.Include(includedProperty);
                }
            }

            if (orderBy is not null)
            {
                return await orderBy(query).AsNoTracking().ToPagedListAsync(requestParams.PageSize, requestParams.PageNumber);
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageSize, requestParams.PageNumber);
        }

        public async Task InsertRangeAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbSet;
            query = query.Where(predicate);
            _dbSet.RemoveRange(query);
        }

        public async Task<IEnumerable<TEntity>> GetRangeAsync(Expression<Func<TEntity, bool>>? predicate = null, List<string>? includedProps = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (includedProps is not null)
            {
                foreach (var includedProperty in includedProps)
                {
                    query = query.Include(includedProperty);
                }
            }

            if (orderBy is not null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var entityToDelete = await _dbSet.FindAsync(id) ?? throw new ArgumentException(null, nameof(id));
            _dbSet.Remove(entityToDelete);
        }

        public async Task<IDictionary<int, int>> CountGroupByPropIdAsync(Expression<Func<TEntity, int>> keySelector)
        {
            IQueryable<TEntity> query = _dbSet;
            return await query
                .GroupBy(keySelector)
                .Select(e => new { Key = e.Key, Count = e.Count() })
                .ToDictionaryAsync(e => e.Key, e => e.Count);          
        }
    }
}
