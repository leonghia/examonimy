﻿using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

namespace ExamonimyWeb.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<PagedList<TEntity>> GetPagedListAsync(RequestParams? requestParams, Expression<Func<TEntity, bool>>? predicate, List<string>? includedProps = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, List<string>? includedProps = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, List<string>? includedProps = null);
        Task<TEntity?> GetSingleByIdAsync(object id);
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
