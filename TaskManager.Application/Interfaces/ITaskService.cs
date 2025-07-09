using TaskManager.Domain.Models;
using Task = TaskManager.Domain.Models.Task;

namespace TaskManager.Application.Interfaces;

public interface ITaskService
{
    
    public Task<IEnumerable<Task>> GetAllAsync();
    
    public Task<Task> GetByIdAsync(int id);
    
    public Task CreateAsync(Task task);
    
    public Task UpdateAsync(Task task);
    
    public Task DeleteAsync(int id);
    
}