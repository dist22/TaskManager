using System;

namespace TaskManager.Application.DTO;

public class UserDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public DateTime CreateAt { get; set; }
    
    public bool IsActive { get; set; }
}