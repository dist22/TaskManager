namespace TaskManager.Application.DTO;

public class UserFilterDto
{
    public string? Name { get; set; } 
    
    public string? Email { get; set; }
    
    public bool? IsActive { get; set; }
}