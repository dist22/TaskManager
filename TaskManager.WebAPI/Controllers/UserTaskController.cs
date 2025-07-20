using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.WebAPI.Extensions;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/usertask")]
[Authorize]

public class UserTaskController(IUserTaskService userTaskService) : ControllerBase
{

    [HttpGet("user/{userId}")]
    public async Task<IEnumerable<TaskTimeDto>> GetTasksByUserIdAsyncController(int userId)
        => await userTaskService.GetTaskByUserIdAsync(userId);

    [HttpGet("log-user")]
    public async Task<IEnumerable<TaskTimeDto>> GetTaskByLoginedUserAsyncController()
        => await userTaskService.GetTaskByUserIdAsync(this.GetUserId());

    [HttpGet("task/{taskId}")]
    public async Task<IEnumerable<UserDto>> GetUserByTaskIdAsyncController(int taskId)
        => await userTaskService.GetUsersByTaskIdAsync(taskId);

    [HttpPatch("{taskId},{userId}/complete")]
    public async Task<IActionResult> CompleteUserTaskAsyncController(int userId, int taskId)
    {
        await userTaskService.CompletedUncompletedUserTaskAsync(userId, taskId, true);
        return Ok();
    }

    [HttpPatch("{taskId},{userId}/uncomplete")]
    public async Task<IActionResult> InCompeteUserTaskAsyncController(int userId, int taskId)
    {
        await userTaskService.CompletedUncompletedUserTaskAsync(userId, taskId);
        return Ok();
    }

    [HttpPost("assigned/{userId}/{taskId}")]
    public async Task<IActionResult> AssignedTaskAsync(int userId, int taskId)
    {
        await userTaskService.AssignTaskAsync(userId, taskId);
        return Ok();
    }

    [HttpDelete("unassigned/{userId}/{taskId}")]
    public async Task<IActionResult> UnassignedTaskAsync(int userId, int taskId)
    {
        await userTaskService.UnassignTaskAsync(userId, taskId);
        return Ok();
    }

}