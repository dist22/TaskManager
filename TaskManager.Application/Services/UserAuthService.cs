using AutoMapper;
using Microsoft.Extensions.Logging;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Interfaces.JwtProvider;
using TaskManager.Application.Interfaces.PasswordHasher;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class UserAuthService(IBaseRepository<User> userRepository, IUserAuthRepository userAuthRepository, IMapper mapper, IPasswordHasher passwordHasher, 
    IJwtProvider jwtProvider, ILogger<UserAuthService> _logger) : BaseService<User>(userRepository, mapper), IUserAuthService
{
    public async Task RegisterUserAsync(UserCreateDto userCreateDto)
    {
        _logger.LogInformation($"Stating user creating: {userCreateDto.Email}");
        if (userCreateDto.Password != userCreateDto.PasswordConfirm)
            throw new Exception("Passwords do not match!");
        
        var emailExist = await userAuthRepository.IfExistAsync(u => u.Email == userCreateDto.Email);
        if(emailExist)
            throw new Exception("Email already exist!");
        
        var user = mapper.Map<User>(userCreateDto);
        await EnsureSuccess( userRepository.AddAsync(user));
        
        _logger.LogInformation($"User successful created");

        var userAuth = new UserAuth
        {
            UserId = user.Id,
            Email = userCreateDto.Email,
            Password = passwordHasher.GeneratePasswordHash(userCreateDto.Password)
        };
        await EnsureSuccess(userAuthRepository.AddAsync(userAuth));
        
        _logger.LogInformation($"Auth info successful added to UserAuth table");
    }

    public async Task<Dictionary<string, string>> LoginUserAsync(UserLoginDto userLoginDto)
    {
        var userAuth = await GetIfNotNull(userAuthRepository.GetUserAuthAsync(userLoginDto.Email));
        var passwordMatch = passwordHasher.Verify(userLoginDto.Password, userAuth.Password);
        if(!passwordMatch)
            throw new Exception("Passwords do not match!");
        
        var token = jwtProvider.CreateToken(userAuth.User);

        _logger.LogInformation($"User {userLoginDto.Email} has been login");
        
        return new Dictionary<string, string>
        {
            { "token", token }
        };

    }
}