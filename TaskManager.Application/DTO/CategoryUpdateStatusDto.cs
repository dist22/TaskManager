using TaskManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.DTO;

public class CategoryUpdateStatusDto
{
    [Required]
    public ActiveStatus ActiveStatus { get; set; }
}