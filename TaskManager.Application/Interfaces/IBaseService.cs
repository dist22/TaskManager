using System.Linq.Expressions;

namespace TaskManager.Application.Interfaces;

public interface IBaseService<T> where T : class
{
    public Task<U> GetByPredicateAsync<U>(Expression<Func<T, bool>> predicate);

    public Task<IEnumerable<U>> GetAllAsync<U>();

}