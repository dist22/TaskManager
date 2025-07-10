using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class TaskServices(IBaseRepository<TaskTime> taskRepository, IBaseRepository<Category> categoryRepository,
    IMapper mapper) : ITaskService
{
    public async Task<IEnumerable<TaskTime>> GetAllAsync() 
        => await taskRepository.GetAllAsync();

    public async Task<TaskTime> GetByIdAsync(int id) 
        => await taskRepository.GetAsync(t => t.Id == id) ?? 
           throw new Exception("Task not found");

    public async Task CreateAsync(TaskTimeCreateDto createDto)
    {

        var categoryExist = await categoryRepository
            .IfExistAsync(c => c.Id == createDto.CategoryId && c.IsActive);
        
        if(!categoryExist)
            throw new Exception("Category not found");
        
        var category = await categoryRepository.GetAsync(c => c.Id == createDto.CategoryId);

        var task = new TaskTime
        {
            Title = createDto.Title,
            Description = createDto.Description,
            CategoryName = category.Name,
            CategoryId = createDto.CategoryId,
        };
        
        var result = await taskRepository.AddAsync(task);
        if(!result)
            throw new Exception("404");

    }

    public async Task UpdateAsync(TaskTimeEditDto taskTime)
    {
        var result = await taskRepository.UpdateAsync(mapper.Map<TaskTime>(taskTime));
        if (!result)
            throw new Exception("Task not found");
    }

    public async Task DeleteAsync(int id)
    {
        var result = await taskRepository.DeleteAsync(
            await taskRepository.GetAsync(t => t.Id == id));
        if(!result)
            throw new Exception("Task not found");
    }
}