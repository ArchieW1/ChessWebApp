using AutoMapper;
using ChessWebApp.DbApi.Models;
using ChessWebApp.Shared.Dtos;

namespace ChessWebApp.DbApi.Profiles;

public sealed class UsersProfile : Profile
{
    public UsersProfile()
    {
        // Source->Target
        CreateMap<User, UserReadDto>();
        CreateMap<User, UserReadPasswordDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}