using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class CategoryService(
    IBaseRepository<Category> categoryRepository,
    IMapper mapper) : ICategoryService
{
    public async Task<Category> GetAsync(int id)
        => await categoryRepository.GetAsync(c => c.Id == id);

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await categoryRepository.GetAllAsync();

    public async Task Create(CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
        var result = await categoryRepository.AddAsync(
            mapper.Map<Category>(categoryCreateUpdateDto));
        if (!result)
            throw new Exception("Category could not be created");
    }

    public async Task Update(CategoryCreateUpdateDto categoryCreateUpdateDto, int Id)
    {
        var category = await categoryRepository.GetAsync(c => c.Id == Id);
        
        category.Name = categoryCreateUpdateDto.Name;
        category.Description = categoryCreateUpdateDto.Description;
        
        var result = await categoryRepository.UpdateAsync(category);
        if(!result)
            throw new Exception("Category could not be updated");
    }

    public async Task ChangeStatus(int Id, ActiveStatus activeStatus)
    {
        var category = await categoryRepository.GetAsync(c => c.Id == Id);
        
        category.IsActive = activeStatus == ActiveStatus.Active;
        
        await EnsureSuccess(categoryRepository.UpdateAsync(category));
    }

    public async Task Delete(int id)
    {
        var result = await categoryRepository.DeleteAsync(
            await categoryRepository.GetAsync(c => c.Id == id));
        if(!result)
            throw new Exception("Category could not be deleted");
    }
}