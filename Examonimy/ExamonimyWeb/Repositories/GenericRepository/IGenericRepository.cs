using System.Linq.Expressions;

namespace ExamonimyWeb.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, List<string>? includedProperties);
        Task InsertAsync(TEntity entity);
        Task SaveAsync();
    }
}
