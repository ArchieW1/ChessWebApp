using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Domain.Common;

namespace ChessWebApp.Api.Mapping;

public static class ApiContractToDomainMapper
{
    public static User ToUser(this CreateUserRequest request)
    {
        return new User
        {
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            Password = Password.From(request.Password),
            Wins = Wins.From(0),
            Losses = Losses.From(0)
        };
    }

    public static User ToUser(this UpdateUserRequest request)
    {
        return new User
        {
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            Password = Password.From(request.Password),
            Wins = Wins.From(request.Wins),
            Losses = Losses.From(request.Losses)
        };
    }
}
