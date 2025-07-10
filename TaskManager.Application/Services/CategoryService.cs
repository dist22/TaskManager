using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class CategoryService(
    IBaseRepository<Category> categoryRepository,
    IMapper mapper) : BaseService, ICategoryService
{
    public async Task<Category> GetAsync(int id)
        => await GetIfNotNull(categoryRepository.GetAsync(c => c.Id == id));

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await categoryRepository.GetAllAsync();

    public async Task Create(CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
        await EnsureSuccess(categoryRepository.AddAsync(
            mapper.Map<Category>(categoryCreateUpdateDto)));
    }

    public async Task Update(CategoryCreateUpdateDto categoryCreateUpdateDto, int Id)
    {
        var category = await GetIfNotNull(categoryRepository.GetAsync(c => c.Id == Id));
        
        category.Name = categoryCreateUpdateDto.Name;
        category.Description = categoryCreateUpdateDto.Description;
        
        await EnsureSuccess( categoryRepository.UpdateAsync(category));
    }

    public async Task ChangeStatus(int Id, ActiveStatus activeStatus)
    {
        var category = await categoryRepository.GetAsync(c => c.Id == Id);
        
        category.IsActive = activeStatus == ActiveStatus.Active;
        
        await EnsureSuccess(categoryRepository.UpdateAsync(category));
    }

    public async Task Delete(int id)
    {
        await EnsureSuccess(categoryRepository.DeleteAsync(
            await categoryRepository.GetAsync(c => c.Id == id)));
    }
}