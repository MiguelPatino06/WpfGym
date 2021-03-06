﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PowerClub.DataAccess.Utils
{
    public interface IGenericFactoryAsync<T, in TKey> : IGenericFactory<T, TKey> where T : class
    {
        Task SaveAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<T> GetByIdAsync(TKey id);

        Task<T> FirstAsync(params Expression<Func<T, bool>>[] filters);
        Task<TR> FirstAsync<TR>(params Expression<Func<TR, bool>>[] filters) where TR : class, T;

        Task<T> FirstAsync(IEnumerable<Expression<Func<T, object>>> includeProperties, params Expression<Func<T, bool>>[] filters);
        Task<TR> FirstAsync<TR>(IEnumerable<Expression<Func<TR, object>>> includeProperties, params Expression<Func<TR, bool>>[] filters) where TR : class, T;

        Task<int> CountAsync(params Expression<Func<T, bool>>[] filters);
    }
}
