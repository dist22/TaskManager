using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class UserTaskService(IBaseRepository<User> userRepository, IBaseRepository<TaskTime> taskRepository, 
    IUserTaskRepository userTaskRepository, IMapper mapper) : IUserTaskService
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
    }

    public async Task UnassignTaskAsync(int userId, int taskId)
    {
        await UserTaskExists(userId, taskId);
        
        var userTask = await GetUserTask(userId, taskId);
        
        await userTaskRepository.DeleteAsync(userTask);
    }

    public async Task<IEnumerable<TaskTimeDto>> GetTaskByUserIdAsync(int userId)
    {
        var taskList = await userTaskRepository.GetTasksByUserIdAsync(userId);

        return taskList.Select(ut => new TaskTimeDto
        {
            Id = ut.TaskId,
            Title = ut.Task.Title,
            Description = ut.Task.Description,
            CreateAt = ut.Task.CreateAt,
            DueDate = ut.Task.DueDate,
            Priority = ut.Task.Priority,
            IsCompleted = ut.IsCompeted,
        });
    }

    public async Task<IEnumerable<UserDto>> GetUsersByTaskIdAsync(int taskId)
    {
        var userList = await userTaskRepository.GetUsersByTaskIdAsync(taskId);

        return userList.Select(ut => new UserDto
        {
            Id = ut.User.Id,
            Name = ut.User.Name,
            Email = ut.User.Email,
            CreateAt = ut.User.CreateAt,
            IsActive = ut.User.IsActive,
        });
    }

    public async Task CompleteUserTaskAsync(int userId, int taskId)
    {
        var userTask = await GetUserTask(userId, taskId);
        
        userTask.IsCompeted = true;
        userTask.CompetedAt = DateTime.Now;
        
        await userTaskRepository.UpdateAsync(userTask);
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