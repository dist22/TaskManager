using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTO;

public class UserFilterSortDto
{
    public string? Name { get; set; } 
    public string? Email { get; set; }
    public bool? IsActive { get; set; }
    public UserSortBy SortBy { get; set; } = UserSortBy.Id;
    public bool Descending { get; set; } = false;
}