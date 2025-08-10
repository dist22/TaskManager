using System;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTO;

public class TaskTimeEditDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public int CategoryId { get; set; }
}