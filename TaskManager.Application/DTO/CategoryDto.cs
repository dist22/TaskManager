namespace TaskManager.Application.DTO;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public bool IsActive { get; set; } = true;
}