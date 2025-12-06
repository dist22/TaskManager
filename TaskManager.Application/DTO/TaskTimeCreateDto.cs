using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTO;

public class TaskTimeCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DeadLineOptions DueDate {get;set;} =  DeadLineOptions.OneWeek;
    public int CategoryId { get; set; }
}