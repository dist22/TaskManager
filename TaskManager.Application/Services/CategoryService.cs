using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class CategoryService(IBaseRepository<Category> categoryRepository, IMapper mapper, ILogger<CategoryService> _logger) 
    : BaseService<Category>(categoryRepository, mapper), ICategoryService
{
    public async Task Create(CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
        await EnsureSuccess(categoryRepository.AddAsync(
            mapper.Map<Category>(categoryCreateUpdateDto)));
        _logger.LogInformation("New category Created");
    }

    public async Task Update(CategoryCreateUpdateDto categoryCreateUpdateDto, int id)
    {
        var category = await GetIfNotNull(categoryRepository.GetAsync(c => c.Id == id));
        
        category.Name = categoryCreateUpdateDto.Name;
        category.Description = categoryCreateUpdateDto.Description;
        
        await EnsureSuccess( categoryRepository.UpdateAsync(category));
        _logger.LogInformation($"Update category: {category.Name}");
    }

    public IEnumerable<CategoryDto> FilterSortCategory(CategoryFilterSortDto filterSortDto)
    {
        
        _logger.LogInformation($"Start task filter-sort: {filterSortDto}");
        
        var query = categoryRepository.GetQueryableAsync();
        
        if (!string.IsNullOrEmpty(filterSortDto.Name))
            query = query.Where(c => c.Name.Contains(filterSortDto.Name));
        if(!string.IsNullOrEmpty(filterSortDto.Description))
            query = query.Where(c => c.Description.Contains(filterSortDto.Description));
        if(filterSortDto.IsActive.HasValue)
            query = query.Where(c => c.IsActive == filterSortDto.IsActive);
        
        _logger.LogInformation($"Finish category filter");

        query = filterSortDto.SortBy switch
        {
            CategorySortBy.Id => filterSortDto.Descending
                ? query.OrderByDescending(c => c.Id)
                : query.OrderBy(c => c.Id),
            CategorySortBy.Name => filterSortDto.Descending
                ? query.OrderByDescending(c => c.Name)
                : query.OrderBy(c => c.Name)
        };
        
        _logger.LogInformation($"Finish category filter-sort");
        
        var categories = query.ToList();
        return mapper.Map<IEnumerable<CategoryDto>>(categories);
        
    }
}