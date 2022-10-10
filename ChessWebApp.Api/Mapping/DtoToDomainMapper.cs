using ChessWebApp.Api.Contracts.Data;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Domain.Common;

namespace ChessWebApp.Api.Mapping;

public static class DtoToDomainMapper
{
    public static User ToUser(this UserDto userDto)
    {
        return new User
        {
            Email = EmailAddress.From(userDto.Email),
            Username = Username.From(userDto.Username),
            Password = Password.From(userDto.Password),
            Wins = Wins.From(userDto.Wins),
            Losses = Losses.From(userDto.Losses)
        };
    }
}
