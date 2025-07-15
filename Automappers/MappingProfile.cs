using AutoMapper;
using UsersAPI.DTOs;
using UsersAPI.Models;

namespace UsersAPI.Automappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserDto>();
    }
}
