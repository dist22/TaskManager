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

    [HttpGet("category/{id}")]
    public async Task<CategoryDto> GetAsyncController(int id) 
        => await categoryService.GetAsync<CategoryDto>(id);
    
    [HttpGet("all")]
    public async Task<IEnumerable<CategoryDto>> GetAllAsyncController()
        => await categoryService.GetAllAsync<CategoryDto>();

    [HttpGet("active")]
    public async Task<IEnumerable<CategoryDto>> GetActiveAsyncController()
        => await categoryService.GetAllActiveAsync<CategoryDto>();
 
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsyncController([FromForm] CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
        await categoryService.Create(categoryCreateUpdateDto);
        return Ok();
    }

    [HttpPut("category/{id}/edit")]
    public async Task<IActionResult> EditCategoryAsyncController(int id, [FromForm] CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
        await categoryService.Update(categoryCreateUpdateDto, id);
        return Ok();
    }

    [HttpPatch("category/{id}/change-status")]
    public async Task<IActionResult> ChangeStatusAsyncController(int id, ActiveStatus activeStatus)
    {
        await categoryService.ChangeStatusAsync(id, activeStatus);
        return Ok();
    }


    [HttpDelete("category/{id}")]
    public async Task<IActionResult> DeleteCategoryAsyncController(int id)
    {
        await categoryService.DeleteAsync(id);
        return Ok();
    }

}