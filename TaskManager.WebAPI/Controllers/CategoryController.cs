using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/categories")]

public class CategoryController(ICategoryService categoryService) : ControllerBase
{

    [HttpGet("all")]
    public async Task<IEnumerable<Category>> GetAll()
        => await categoryService.GetAllAsync();
    
    [HttpGet("category/{id}")]
    public async Task<Category> Get(int id) 
        => await categoryService.GetAsync(id);

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
        await categoryService.Create(categoryCreateUpdateDto);
        return Ok();
    }

    [HttpPut("category/{id}/edit")]
    public async Task<IActionResult> Put(int id, [FromForm] CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
        await categoryService.Update(categoryCreateUpdateDto, id);
        return Ok();
    }

    [HttpPatch("category/{id}/change-status")]
    public async Task<IActionResult> ChangeStatus(int id, ActiveStatus activeStatus)
    {
        await categoryService.ChangeStatus(id, activeStatus);
        return Ok();
    }


    [HttpDelete("category/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await categoryService.Delete(id);
        return Ok();
    }

}