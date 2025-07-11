using TaskManager.Domain.Models;
using TaskManager.Application.DTO;

namespace TaskManager.Application.Interfaces;

public interface ITaskService : IBaseService<TaskTime>
{
   
    public Task CreateAsync(TaskTimeCreateDto taskTime);
    
    public Task UpdateAsync(TaskTimeEditDto taskTime);
    
    public Task DeleteAsync(int id);
    
}