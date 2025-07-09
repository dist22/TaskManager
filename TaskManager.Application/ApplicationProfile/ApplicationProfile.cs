using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Domain.Models;
namespace TaskManager.Application.ApplicationProfile;

public class ApplicationProfile : Profile
{

    public ApplicationProfile()
    {
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserCreateDto>();

        CreateMap<UserEditDto, User>();
        CreateMap<User, UserEditDto>();
    }
    
}