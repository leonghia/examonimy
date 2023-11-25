using ExamonimyWeb.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamonimyWeb.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ExamonimyContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ExamonimyContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, List<string>? includedProperties)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where<T>(predicate);          

            if (includedProperties is not null)
            {
                foreach (var includedProperty in includedProperties) 
                {
                    query = query.Include<T>(includedProperty);
                }
            }

            return await query.FirstOrDefaultAsync<T>();

        }
    }
}
