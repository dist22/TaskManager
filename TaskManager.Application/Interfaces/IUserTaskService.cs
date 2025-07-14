using TaskManager.Domain.Models;
using TaskManager.Application.Interfaces;
using TaskManager.Application.DTO;

namespace TaskManager.Application.Interfaces;

public interface IUserTaskService
{
    public Task AssignTaskAsync(int userId, int taskId);
    public Task UnassignTaskAsync(int userId, int taskId);
    public Task<IEnumerable<TaskTimeDto>> GetTaskByUserIdAsync(int userId);
    public Task<IEnumerable<UserDto>> GetUsersByTaskIdAsync(int taskId);
    public Task CompleteUserTaskAsync(int userId, int taskId);

}