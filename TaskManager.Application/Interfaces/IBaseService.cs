using System.Linq.Expressions;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Interfaces;

public interface IBaseService<T> where T : class
{
    public Task<U> GetByPredicateAsync<U>(Expression<Func<T, bool>> predicate);

    public Task<IEnumerable<U>> GetAllAsync<U>();
    
    public Task DeleteAsync(Expression<Func<T, bool>> predicate);

}