using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services;

public abstract class BaseService<T>(IBaseRepository<T> repository, IMapper mapper)
    : IBaseService<T> where T : class, IEntity
{
    protected async Task<U> GetIfNotNull<U>(Task<U> task)
    {
        var result = await task;
        if (result != null) return result;
        throw new NotFoundException($"Object not found");
    }

    protected async Task EnsureSuccess(Task<bool> task)
    {
        var result = await task;
        if (result) return;
        throw new ConflictException("Task result is false");
    }

    public virtual async Task<U> GetAsync<U>(int id)
        => mapper.Map<U>(await GetIfNotNull(repository.GetAsync(e => e.Id == id)));

    public virtual async Task<IEnumerable<U>> GetAllAsync<U>()
        => mapper.Map<IEnumerable<U>>(await repository.GetAllAsync());
    
    public virtual async Task ChangeStatusAsync(int id, ActiveStatus status)
    {
        var entity = await GetIfNotNull( repository.GetAsync(e => e.Id == id));
        entity.IsActive = status == ActiveStatus.Active;
        await EnsureSuccess(repository.UpdateAsync(entity));
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetIfNotNull(repository.GetAsync(e => e.Id == id));
        await EnsureSuccess(repository.DeleteAsync(entity));
    }
}