using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskManager.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    public Task<bool> UpdateAsync(T @object);
    
    public Task<bool> AddAsync(T @object);

    public Task<T?> GetAsync(Expression<Func<T?, bool>> predicate);
    
    public Task<IEnumerable<T>> GetAllAsync();
    public IQueryable<T> GetQueryableAsync();
    
    public Task<bool> DeleteAsync(T @object);
    
    public Task<bool> IfExistAsync(Expression<Func<T, bool>> expression);
}