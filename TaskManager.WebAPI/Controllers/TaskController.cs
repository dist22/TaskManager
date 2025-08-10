using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]

public class TaskController(ITaskService taskService) : ControllerBase
{
    [HttpGet("task/{id}")]
    public async Task<TaskTimeDto> GetAsyncController(int id)
        => await taskService.GetAsync<TaskTimeDto>(id);
    
    [HttpGet("all")]
    public async Task<IEnumerable<TaskTimeDto>> GetAllAsyncController() 
        => await taskService.GetAllAsync<TaskTimeDto>();
    
    [HttpGet("filter-sort")]
    public IEnumerable<TaskTimeDto> FilterTaskTimeController([FromQuery]TaskTimeFilterSortDto filterSortDto) 
        => taskService.FilterSortTaskTime(filterSortDto);
    
    [HttpPost("task/post")]
    public async Task<IActionResult> CreateTaskAsyncController([FromForm] TaskTimeCreateDto taskTime)
    {
        await taskService.CreateAsync(taskTime);
        return Ok();
    }

    [HttpPatch("task/{id}/change-status")]
    public async Task<IActionResult> ChangeTaskStatusAsyncController(int id, ActiveStatus activeStatus)
    {
        await taskService.ChangeStatusAsync(id, activeStatus);
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