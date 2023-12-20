using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

namespace ExamonimyWeb.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<PagedList<TEntity>> GetPagedListAsync(RequestParams? requestParams, Expression<Func<TEntity, bool>>? searchPredicate, Expression<Func<TEntity, bool>>? filterPredicate, List<string>? includedProperties);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? searchPredicate, Expression<Func<TEntity, bool>>? filterPredicate, List<string>? includedProperties);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filterPredicate, List<string>? includedProperties);
        Task<TEntity?> GetByIdAsync(object id);
        Task InsertAsync(TEntity entity);
        Task InserRangeAsync(List<TEntity> entities);
        void Update(TEntity entity);
        Task SaveAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filterPredicate = null);
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entities);
        void DeleteRange(Expression<Func<TEntity, bool>> predicate);
    }
}
