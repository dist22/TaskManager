using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTO;

public class TaskTimeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } 
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public bool IsCompleted { get; set; }
}