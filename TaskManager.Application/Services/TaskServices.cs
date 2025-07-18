using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class TaskServices(IBaseRepository<TaskTime> taskRepository, IBaseRepository<Category> categoryRepository,
    IMapper mapper) : BaseService<TaskTime>(taskRepository, mapper), ITaskService
{
    public async Task CreateAsync(TaskTimeCreateDto createDto)
    {
        var category = await GetIfNotNull(categoryRepository
            .GetAsync(c => c.Id == createDto.CategoryId && c.IsActive));

        var task = new TaskTime
        {
            Title = createDto.Title,
            Description = createDto.Description,
            CategoryName = category.Name,
            CategoryId = createDto.CategoryId,
        };
        
        await EnsureSuccess( 
            taskRepository.AddAsync(task));
    }

    public async Task UpdateAsync(int id,TaskTimeEditDto taskTime)
    {
        var task = await GetIfNotNull(taskRepository.GetAsync(t => t.Id == id));
        
        var categoryExist =  await categoryRepository.IfExistAsync(c => c.Id == taskTime.CategoryId && c.IsActive);
        if (!categoryExist)
            throw new Exception("Category not found");
        
        task = mapper.Map(taskTime, task);
        
        await EnsureSuccess(taskRepository.UpdateAsync(task));
    }
    
    public IEnumerable<TaskTimeDto> FilterSortTaskTime(TaskTimeFilterSortDto filterSort)
    {
        var query = taskRepository.GetQueryableAsync();
        
        if (!string.IsNullOrEmpty(filterSort.Title))
            query = query.Where(t => t.Title.Contains(filterSort.Title));
        if(!string.IsNullOrEmpty(filterSort.Description))
            query = query.Where(t => t.Description.Contains(filterSort.Description));
        if(!string.IsNullOrEmpty(filterSort.CategoryName))
            query = query.Where(t => t.CategoryName.Contains(filterSort.CategoryName));
        if(filterSort.IsActive.HasValue)
            query = query.Where(t => t.IsActive == filterSort.IsActive);

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
        
        var tasks = query.ToList();
        return mapper.Map<IEnumerable<TaskTimeDto>>(tasks);
        
    }
}