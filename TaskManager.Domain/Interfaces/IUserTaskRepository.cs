using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces;

public interface IUserTaskRepository : IBaseRepository<UserTask>
{
    
    public Task<IEnumerable<UserTask>> GetUsersByTaskIdAsync(int taskId);
    
    public Task<IEnumerable<UserTask>> GetTasksByUserIdAsync(int userId);
    
}