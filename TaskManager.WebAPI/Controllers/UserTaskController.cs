using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/usertask")]

public class UserTaskController(IUserTaskService userTaskService) : ControllerBase
{

    [HttpGet("user/{userId}")]
    public async Task<IEnumerable<TaskTimeDto>> GetTasksByUserIdAsyncController(int userId)
        => await userTaskService.GetTaskByUserIdAsync(userId);

    [HttpGet("task/{taskId}")]
    public async Task<IEnumerable<UserDto>> GetUserByTaskIdAsyncController(int taskId)
        => await userTaskService.GetUsersByTaskIdAsync(taskId);

    [HttpPatch("{taskId},{userId}/complete")]
    public async Task<IActionResult> CompleteUserTaskAsyncController(int userId, int taskId)
    {
        await userTaskService.CompleteUserTaskAsync(userId, taskId);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AssignedTaskAsync(int userId, int taskId)
    {
        await userTaskService.AssignTaskAsync(userId, taskId);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> UnassignedTaskAsync(int userId, int taskId)
    {
        await userTaskService.UnassignTaskAsync(userId, taskId);
        return Ok();
    }

}