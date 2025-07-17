using System.Linq.Expressions;
using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Services;

public class UserServices(IBaseRepository<User> userRepository, IBaseRepository<UserAuth> userAuthRepository, 
    IMapper mapper, IPasswordHasher passwordHasher) : BaseService<User>(userRepository, mapper), IUserServices
{

    public async Task CreateAsync(UserCreateDto userCreate)
    {
        if (userCreate.Password != userCreate.PasswordConfirm)
            throw new Exception("Passwords do not match!");

        var user = mapper.Map<User>(userCreate);
        await EnsureSuccess( userRepository.AddAsync(user));

        var userAuth = new UserAuth
        {
            UserId = user.Id,
            Email = userCreate.Email,
            Password = passwordHasher.GeneratePasswordHash(userCreate.Password)
        };
        await EnsureSuccess(userAuthRepository.AddAsync(userAuth));
        
    }

    public async Task UpdateAsync(UserEditDto userEditDto, int id)
    {

        var user = await GetIfNotNull(userRepository.GetAsync(u => u.Id == id));
        user = mapper.Map(userEditDto, user);
        await EnsureSuccess(userRepository.UpdateAsync(user));

    }

    public IEnumerable<UserDto> FilterAsync(UserFilterDto filter)
    {
        var query = userRepository.GetQueryableAsync();
        
        if (!string.IsNullOrEmpty(filter.Email))
            query = query.Where(u => u.Email.Contains(filter.Email));
        if(!string.IsNullOrEmpty(filter.Name))
            query = query.Where(u => u.Email.Contains(filter.Name));
        if (filter.IsActive.HasValue)
            query = query.Where(u => u.IsActive == filter.IsActive);

        var users = query.ToList();

        return mapper.Map<IEnumerable<UserDto>>(users);
        
    }
}