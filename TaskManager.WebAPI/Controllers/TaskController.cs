using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/tasks")]

public class TaskController(ITaskService taskService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IEnumerable<TaskTime>> GetAll() 
        => await taskService.GetAllAsync();

    [HttpPost("task/{id}")]
    public async Task<TaskTime> GetAsync(int id)
        => await taskService.GetByIdAsync(id);

    [HttpPost("task/post")]
    public async Task<IActionResult> PostTaskAsync([FromForm] TaskTimeCreateDto taskTime)
    {
        await taskService.CreateAsync(taskTime);
        return Ok();
    }

    [HttpPut("task-edit")]
    public async Task<IActionResult> UpdateAsync([FromForm] TaskTimeEditDto taskTime)
    {
        await taskService.UpdateAsync(taskTime);
        return Ok();
    }

    [HttpDelete("task/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await taskService.DeleteAsync(id);
        return Ok();
    }
}