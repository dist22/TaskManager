namespace TaskManager.Application.DTO;

public class TaskTimeCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}