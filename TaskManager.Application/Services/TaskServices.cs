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
}