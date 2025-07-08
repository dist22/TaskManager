using AutoMapper;
using TaskManager.Application.DTO_s;
using TaskManager.Domain.Models;

namespace TaskManager.Application.ApplicationProfile;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserCreateDto>();
    }
}