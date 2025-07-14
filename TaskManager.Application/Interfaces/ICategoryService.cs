using TaskManager.Domain.Models;
using TaskManager.Domain.Enums;
using TaskManager.Application.DTO;

namespace TaskManager.Application.Interfaces;

public interface ICategoryService : IBaseService<Category>
{
    public Task Create(CategoryCreateUpdateDto categoryCreateUpdateDto);
    
    public Task Update(CategoryCreateUpdateDto categoryCreateUpdateDto, int id);
    
}