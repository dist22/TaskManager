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

    public async Task<IEnumerable<TaskTimeDto>> GetSortedAsync(TaskSortBy sortBy, bool desc = false)
    {
        var tasks = await taskRepository.GetAllAsync();

        tasks = sortBy switch
        {
            TaskSortBy.Id => desc ? tasks.OrderByDescending(t => t.Id) : tasks.OrderBy(t => t.Id),
            TaskSortBy.Title => desc ? tasks.OrderByDescending(t => t.Title) : tasks.OrderBy(t => t.Title),
            TaskSortBy.DueDate => desc ? tasks.OrderByDescending(t => t.DueDate) : tasks.OrderBy(t => t.DueDate),
            TaskSortBy.Priority => desc ? tasks.OrderByDescending(t => t.Priority) : tasks.OrderBy(t => t.Priority),
        };
        
        return mapper.Map<IEnumerable<TaskTimeDto>>(tasks);
    }

    public IEnumerable<TaskTimeDto> FilterTaskTime(TaskTimeFilterDto filter)
    {
        var query = taskRepository.GetQueryableAsync();
        
        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(t => t.Title.Contains(filter.Title));
        if(!string.IsNullOrEmpty(filter.Description))
            query = query.Where(t => t.Description.Contains(filter.Description));
        if(!string.IsNullOrEmpty(filter.CategoryName))
            query = query.Where(t => t.CategoryName.Contains(filter.CategoryName));
        if(filter.IsActive.HasValue)
            query = query.Where(t => t.IsActive == filter.IsActive);
        
        var tasks = query.ToList();
        return mapper.Map<IEnumerable<TaskTimeDto>>(tasks);
        
    }
}