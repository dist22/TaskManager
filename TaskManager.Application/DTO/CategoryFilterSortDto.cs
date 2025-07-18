using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTO;

public class CategoryFilterSortDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public CategorySortBy? SortBy { get; set; } = CategorySortBy.Id;
    public bool Descending { get; set; } = false;
}