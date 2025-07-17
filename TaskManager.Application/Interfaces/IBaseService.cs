using System.Linq.Expressions;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Interfaces;

public interface IBaseService<T> where T : class
{
    public Task<U> GetAsync<U>(int id);

    public Task<IEnumerable<U>> GetAllAsync<U>();
    
    public Task DeleteAsync(int id);

    public Task ChangeStatusAsync(int id, ActiveStatus status);

}