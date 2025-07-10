using TaskManager.Domain.Models;
using TaskManager.Domain.Enums;
using TaskManager.Application.DTO;

namespace TaskManager.Application.Interfaces;

public interface ICategoryService
{
    public Task<Category> GetAsync(int id);

    public Task<IEnumerable<Category>> GetAllAsync();

    public Task Create(CategoryCreateUpdateDto categoryCreateUpdateDto);

    public Task ChangeStatus(int Id, ActiveStatus activeStatus);

    public Task Update(CategoryCreateUpdateDto categoryCreateUpdateDto, int Id);

    public Task Delete(int id);
}