using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/tasks")]

public class TaskController(ITaskService taskService) : ControllerBase
{
    [HttpGet("task/{id}")]
    public async Task<TaskTimeDto> GetAsyncController(int id)
        => await taskService.GetAsync<TaskTimeDto>(id);
    
    [HttpGet("all")]
    public async Task<IEnumerable<TaskTimeDto>> GetAllAsyncController() 
        => await taskService.GetAllAsync<TaskTimeDto>();

    [HttpGet("active")]
    public async Task<IEnumerable<TaskTimeDto>> GetActiveAsyncController()
        => await taskService.GetAllActiveAsync<TaskTimeDto>();

    [HttpPost("task/post")]
    public async Task<IActionResult> CreateTaskAsyncController([FromForm] TaskTimeCreateDto taskTime)
    {
        await taskService.CreateAsync(taskTime);
        return Ok();
    }

    [HttpPut("task/{id}/edit")]
    public async Task<IActionResult> EditTaskAsyncController(int id, [FromForm] TaskTimeEditDto taskTimeEditDto)
    {
        await taskService.UpdateAsync(id, taskTimeEditDto);
        return Ok();
    }

    [HttpDelete("task/{id}")]
    public async Task<IActionResult> DeleteAsyncController(int id)
    {
        await taskService.DeleteAsync(id);
        return Ok();
    }
}