using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class CategoryService(IBaseRepository<Category> categoryRepository, IMapper mapper) 
    : BaseService<Category>(categoryRepository, mapper), ICategoryService
{
    public async Task Create(CategoryCreateUpdateDto categoryCreateUpdateDto) 
        => await EnsureSuccess(categoryRepository.AddAsync(
            mapper.Map<Category>(categoryCreateUpdateDto)));

    public async Task Update(CategoryCreateUpdateDto categoryCreateUpdateDto, int id)
    {
        var category = await GetIfNotNull(categoryRepository.GetAsync(c => c.Id == id));
        
        category.Name = categoryCreateUpdateDto.Name;
        category.Description = categoryCreateUpdateDto.Description;
        
        await EnsureSuccess( categoryRepository.UpdateAsync(category));
    }

    public IEnumerable<CategoryDto> FilterCategory(CategoryFilterDto filterDto)
    {
        var query = categoryRepository.GetQueryableAsync();
        
        if (!string.IsNullOrEmpty(filterDto.Name))
            query = query.Where(c => c.Name.Contains(filterDto.Name));
        if(!string.IsNullOrEmpty(filterDto.Description))
            query = query.Where(c => c.Description.Contains(filterDto.Description));
        
        if(filterDto.IsActive.HasValue)
            query = query.Where(c => c.IsActive == filterDto.IsActive);
        
        var categories = query.ToList();
        return mapper.Map<IEnumerable<CategoryDto>>(categories);
        
    }
}