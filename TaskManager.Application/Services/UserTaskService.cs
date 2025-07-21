using AutoMapper;
using Microsoft.Extensions.Logging;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class UserTaskService(IBaseRepository<User> userRepository, IBaseRepository<TaskTime> taskRepository, 
    IUserTaskRepository userTaskRepository, ILogger<UserTaskService> _logger) : IUserTaskService
{
    public async Task AssignTaskAsync(int userId, int taskId)
    {
        
        await UserTaskExists(userId, taskId);
        
        var alreadyAssigned = await userTaskRepository.IfExistAsync(
            ut => ut.UserId == userId && ut.TaskId == taskId);
        
        if(alreadyAssigned)
            throw new Exception("Task already assigned for this user");

        var userTask = new UserTask
        {
            UserId = userId,
            TaskId = taskId,
            AssignedAt = DateTime.Now,
            IsCompeted = false
        };
        
        await userTaskRepository.AddAsync(userTask);
        
        _logger.LogInformation($"Assigned task {taskId} to user {userId}");
    }

    public async Task UnassignTaskAsync(int userId, int taskId)
    {
        await UserTaskExists(userId, taskId);
        
        var userTask = await GetUserTask(userId, taskId);
        
        await userTaskRepository.DeleteAsync(userTask);
        
        _logger.LogInformation($"Unassign task {taskId} from user {userId}");
    }

    public async Task<IEnumerable<TaskTimeDto>> GetTaskByUserIdAsync(int userId)
    {
        var taskList = await userTaskRepository.GetTasksByUserIdAsync(userId);
        
        _logger.LogInformation($"Getting tasks for user {userId}");

        return taskList.Select(ut => new TaskTimeDto
        {
            Id = ut.TaskId,
            Title = ut.Task.Title,
            Description = ut.Task.Description,
            CreateAt = ut.Task.CreateAt,
            DueDate = ut.Task.DueDate,
            Priority = ut.Task.Priority,
            IsActive = ut.Task.IsActive
        });
    }

    public async Task<IEnumerable<UserDto>> GetUsersByTaskIdAsync(int taskId)
    {
        var userList = await userTaskRepository.GetUsersByTaskIdAsync(taskId);
        _logger.LogInformation($"Getting users for task {taskId}");

        return userList.Select(ut => new UserDto
        {
            Id = ut.User.Id,
            Name = ut.User.Name,
            Email = ut.User.Email,
            CreateAt = ut.User.CreateAt,
            IsActive = ut.User.IsActive,
        });
    }

    public async Task CompletedUncompletedUserTaskAsync(int userId, int taskId, bool completed = false)
    {
        var userTask = await GetUserTask(userId, taskId);

        if (completed)
        {
            _logger.LogInformation($"Task {taskId} has been completed");
            userTask.IsCompeted = true;
            userTask.CompetedAt = DateTime.Now;
        }
        else
        {
            _logger.LogInformation($"Task {taskId} has not been completed");
            userTask.IsCompeted = false;
            userTask.CompetedAt = null;
        }
        
        await userTaskRepository.UpdateAsync(userTask);
        _logger.LogInformation($"Task {taskId}, status has been updated from user {userId}");
    }

    private async Task UserTaskExists(int userId, int taskId)
    {
        var userExist = await userRepository.IfExistAsync(u => u.Id == userId && u.IsActive == true);
        var taskExist =  await taskRepository.IfExistAsync(t => t.Id == taskId && t.IsActive == true);

        if (!userExist && !taskExist)
            throw new Exception("User or task not exist");
    }

    private async Task<UserTask> GetUserTask(int userId, int taskId) 
        => await userTaskRepository.GetAsync(ut => ut.UserId == userId && ut.TaskId == taskId) ??
            throw new Exception("User or task not exist");

}