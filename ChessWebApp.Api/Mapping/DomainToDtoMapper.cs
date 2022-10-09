using ChessWebApp.Api.Contracts.Data;
using ChessWebApp.Api.Domain;

namespace ChessWebApp.Api.Mapping;

public static class DomainToDtoMapper
{
    public static UserDto ToCustomerDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id.Value.ToString(),
            Email = user.Email.Value,
            Username = user.Username.Value,
            Password = user.Password.Value,
            Wins = user.Wins.Value,
            Losses = user.Losses.Value
        };
    }
}
