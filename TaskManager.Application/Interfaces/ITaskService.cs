using TaskManager.Domain.Models;
using TaskManager.Application.DTO;

namespace TaskManager.Application.Interfaces;

public interface ITaskService
{
    public Task<IEnumerable<TaskTime>> GetAllAsync();
    
    public Task<TaskTime> GetByIdAsync(int id);
    
    public Task CreateAsync(TaskTimeCreateDto taskTime);
    
    public Task UpdateAsync(TaskTimeEditDto taskTime);
    
    public Task DeleteAsync(int id);
    
}