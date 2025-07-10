using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Enums;

public enum ActiveStatus
{
    [Display(Name = "Active")]
    Active = 1,
    
    [Display(Name = "Inactive")]
    Inactive = 0
}