using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTO;

public class TaskTimeFilterSortDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; } 
    public bool? IsActive { get; set; }
    public TaskSortBy? SortBy { get; set; } = TaskSortBy.Id;
    public bool Descending { get; set; } = false;

}