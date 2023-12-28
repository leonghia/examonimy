using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

namespace ExamonimyWeb.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<PagedList<TEntity>> GetPagedListAsync(RequestParams? requestParams, Expression<Func<TEntity, bool>>? searchPredicate, Expression<Func<TEntity, bool>>? filterPredicate, List<string>? includedProperties = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderByPredicate = null);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? searchPredicate = null, Expression<Func<TEntity, bool>>? filterPredicate = null, List<string>? includedProperties = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderByPredicate = null);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filterPredicate, List<string>? includedProperties = null);
        Task<TEntity?> GetByIdAsync(object id);
        Task InsertAsync(TEntity entity);
        Task InsertRangeAsync(List<TEntity> entities);
        void Update(TEntity entity);
        Task SaveAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filterPredicate = null);
        Task DeleteAsync (object id);
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entities);
        void DeleteRange(Expression<Func<TEntity, bool>> predicate);
    }
}
