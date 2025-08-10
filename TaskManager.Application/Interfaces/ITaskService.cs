using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Domain.Models;
using TaskManager.Application.DTO;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Interfaces;

public interface ITaskService : IBaseService<TaskTime>
{
    public Task CreateAsync(TaskTimeCreateDto taskTime);
    
    public Task UpdateAsync(int id, TaskTimeEditDto taskTime);
    
    public IEnumerable<TaskTimeDto> FilterSortTaskTime(TaskTimeFilterSortDto filterSort);
    
}