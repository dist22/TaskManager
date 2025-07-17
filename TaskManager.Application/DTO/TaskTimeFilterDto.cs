namespace TaskManager.Application.DTO;

public class TaskTimeFilterDto
{
    public string? Title { get; set; } 
    public string? Description { get; set; } 
    public string? CategoryName { get; set; } 
    public bool? IsActive { get; set; }
}