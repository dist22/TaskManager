using System.Linq.Expressions;
using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Services;

public class UserServices(IBaseRepository<User> userRepository, IBaseRepository<UserAuth> userAuthRepository, 
    IMapper mapper, IPasswordHasher passwordHasher) : BaseService<User>(userRepository, mapper), IUserServices
{
    public async Task UpdateAsync(UserEditDto userEditDto, int id)
    {

        var user = await GetIfNotNull(userRepository.GetAsync(u => u.Id == id));
        user = mapper.Map(userEditDto, user);
        await EnsureSuccess(userRepository.UpdateAsync(user));

    }

    public IEnumerable<UserDto> FilterSortUser(UserFilterSortDto filterSort)
    {
        var query = userRepository.GetQueryableAsync();
        
        if (!string.IsNullOrEmpty(filterSort.Email))
            query = query.Where(u => u.Email.Contains(filterSort.Email));
        if(!string.IsNullOrEmpty(filterSort.Name))
            query = query.Where(u => u.Email.Contains(filterSort.Name));
        if (filterSort.IsActive.HasValue)
            query = query.Where(u => u.IsActive == filterSort.IsActive);

        query = filterSort.SortBy switch
        {
            UserSortBy.Id => filterSort.Descending
                ? query.OrderByDescending(t => t.Id)
                : query.OrderBy(t => t.Id),
            UserSortBy.Email => filterSort.Descending 
                ? query.OrderByDescending(t => t.Email)
                : query.OrderBy(t => t.Email),
            UserSortBy.Name => filterSort.Descending 
                ?  query.OrderByDescending(t => t.Name)
                :  query.OrderBy(t => t.Name)
        };

        var users = query.ToList();
        return mapper.Map<IEnumerable<UserDto>>(users);
        
    }
}