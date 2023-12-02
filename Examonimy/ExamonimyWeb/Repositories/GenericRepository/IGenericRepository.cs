﻿using ExamonimyWeb.Models;
using System.Linq.Expressions;

namespace ExamonimyWeb.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<PagedList<TEntity>> GetAsync(RequestParams? requestParams, Expression<Func<TEntity, bool>>? filter, List<string>? includedProperties);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, List<string>? includedProperties);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task SaveAsync();
    }
}
