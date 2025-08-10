using System;
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

public class TaskServices(IBaseRepository<TaskTime> taskRepository, IBaseRepository<Category> categoryRepository,
    IMapper mapper, ILogger<TaskServices> _logger) : BaseService<TaskTime>(taskRepository, mapper), ITaskService
{
    public async Task CreateAsync(TaskTimeCreateDto createDto)
    {
        var category = await GetIfNotNull(categoryRepository
            .GetAsync(c => c.Id == createDto.CategoryId && c.IsActive));
        
        _logger.LogInformation("Category for new task completely found");

        var task = new TaskTime
        {
            Title = createDto.Title,
            Description = createDto.Description,
            CategoryName = category.Name,
            CategoryId = createDto.CategoryId,
        };
        
        await EnsureSuccess( 
            taskRepository.AddAsync(task));
        
        _logger.LogInformation("Task successfully created");
    }

    public async Task UpdateAsync(int id,TaskTimeEditDto taskTime)
    {
        var task = await GetIfNotNull(taskRepository.GetAsync(t => t.Id == id));
        
        var categoryExist =  await categoryRepository.IfExistAsync(c => c.Id == taskTime.CategoryId && c.IsActive);
        if (!categoryExist)
            throw new Exception("Category not found");
        
        task = mapper.Map(taskTime, task);
        
        await EnsureSuccess(taskRepository.UpdateAsync(task));
        _logger.LogInformation("Task successfully updated");
    }
    
    public IEnumerable<TaskTimeDto> FilterSortTaskTime(TaskTimeFilterSortDto filterSort)
    {
        
        _logger.LogInformation($"Start task filter-sort: {filterSort}");
        
        var query = taskRepository.GetQueryableAsync();
        
        if (!string.IsNullOrEmpty(filterSort.Title))
            query = query.Where(t => t.Title.Contains(filterSort.Title));
        if(!string.IsNullOrEmpty(filterSort.Description))
            query = query.Where(t => t.Description.Contains(filterSort.Description));
        if(!string.IsNullOrEmpty(filterSort.CategoryName))
            query = query.Where(t => t.CategoryName.Contains(filterSort.CategoryName));
        if(filterSort.IsActive.HasValue)
            query = query.Where(t => t.IsActive == filterSort.IsActive);
        
        _logger.LogInformation($"Finish task filter");

        query = filterSort.SortBy switch
        {
            TaskSortBy.Id => filterSort.Descending 
                ? query.OrderByDescending(t => t.Id) 
                : query.OrderBy(t => t.Id),
            TaskSortBy.Title => filterSort.Descending 
                ? query.OrderByDescending(t => t.Title) 
                : query.OrderBy(t => t.Title),
            TaskSortBy.DueDate => filterSort.Descending
                ? query.OrderByDescending(t => t.DueDate)
                : query.OrderBy(t => t.DueDate),
            TaskSortBy.Priority => filterSort.Descending
                ? query.OrderByDescending(t => t.Priority)
                : query.OrderBy(t => t.Priority),
        };
        _logger.LogInformation($"Finish task filter-sort");
        
        var tasks = query.ToList();
        return mapper.Map<IEnumerable<TaskTimeDto>>(tasks);
        
    }
}