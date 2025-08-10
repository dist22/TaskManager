using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.Application.DTO;
using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Interfaces;

public interface IUserServices : IBaseService<User>
{
    public Task UpdateAsync(UserEditDto userEditDto, int id);
    
    public IEnumerable<UserDto> FilterSortUser(UserFilterSortDto userFilterSortDto);
    
}