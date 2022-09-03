using AutoMapper;
using ChessWebApp.DbApi.Dtos;
using ChessWebApp.DbApi.Models;

namespace ChessWebApp.DbApi.Profiles;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        // Source->Target
        CreateMap<User, UserReadDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}