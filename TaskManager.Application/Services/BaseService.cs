using System.Linq.Expressions;
using AutoMapper;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services;

public abstract class BaseService<T>(IBaseRepository<T> repository, IMapper mapper)
    : IBaseService<T> where T : class
{
    protected async Task<U> GetIfNotNull<U>(Task<U> task)
    {
        var result = await task;
        if (result != null) return result;
        throw new NullReferenceException();
    }

    protected async Task EnsureSuccess(Task<bool> task)
    {
        var result = await task;
        if (result) return;
        throw new NullReferenceException();
    }

    public virtual async Task<U> GetByPredicateAsync<U>(Expression<Func<T, bool>> predicate)
        => mapper.Map<U>(await GetIfNotNull(repository.GetAsync(predicate)));

    public virtual async Task<IEnumerable<U>> GetAllAsync<U>()
        => mapper.Map<IEnumerable<U>>(await repository.GetAllAsync());
    

    public virtual async Task ChangeStatusAsync(int id, ActiveStatus status)
    {
        var entity = await GetIfNotNull(repository.GetAsync(predicate));
        await EnsureSuccess(repository.DeleteAsync(entity));
    }
}