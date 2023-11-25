using System.Linq.Expressions;

namespace ExamonimyWeb.Repositories.GenericRepository
{
    public interface IGenericRepository<T>
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, List<string>? includedProperties);
    }
}
