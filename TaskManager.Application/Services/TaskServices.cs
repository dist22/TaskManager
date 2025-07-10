using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class TaskServices(IBaseRepository<TaskTime> taskRepository, IBaseRepository<Category> categoryRepository,
    IMapper mapper) : BaseService, ITaskService
{
    public async Task<IEnumerable<TaskTime>> GetAllAsync() 
        => await taskRepository.GetAllAsync();

    public async Task<TaskTime> GetByIdAsync(int id) 
        => await GetIfNotNull(taskRepository.GetAsync(t => t.Id == id));

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

    public async Task UpdateAsync(TaskTimeEditDto taskTime)
    {
        await EnsureSuccess(
            taskRepository.UpdateAsync(mapper.Map<TaskTime>(taskTime)));
    }

    public async Task DeleteAsync(int id)
    {
        await EnsureSuccess(taskRepository.DeleteAsync(
            await taskRepository.GetAsync(t => t.Id == id)));
    }
}