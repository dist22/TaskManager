namespace TaskManager.Application.DTO;

public class UserEditDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}