using System.Globalization;
using ChessWebApp.Api.Authentication;
using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Domain.Common;

namespace ChessWebApp.Api.Mapping;

public static class ApiContractToDomainMapper
{
    public static User ToUser(this CreateUserRequest request)
    {
        string dateNow = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        return new User
        {
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            Password = Password.From(LoginAuthentication.SaltHashSha256(request.Password, dateNow)),
            Salt = Salt.From(dateNow),
            Wins = Wins.From(0),
            Losses = Losses.From(0)
        };
    }

    public static User ToUser(this UpdateUserRequest request)
    {
        string dateNow = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        return new User
        {
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            Password = Password.From(LoginAuthentication.SaltHashSha256(request.Password, dateNow)),
            Salt = Salt.From(dateNow),
            Wins = Wins.From(request.Wins),
            Losses = Losses.From(request.Losses)
        };
    }

    public static User ToUser(this UpdateUserLoginRequest request)
    {
        string dateNow = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        return new User
        {
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            Password = Password.From(LoginAuthentication.SaltHashSha256(request.Password, dateNow)),
            Salt = Salt.From(dateNow),
            Wins = Wins.From(0),
            Losses = Losses.From(0)
        };
    }
    
    public static User ToUser(this UpdateUserStatsRequest request)
    {
        return new User
        {
            Email = EmailAddress.From(string.Empty),
            Username = Username.From(request.Username),
            Password = Password.From(string.Empty),
            Salt = Salt.From(string.Empty),
            Wins = Wins.From(request.Wins),
            Losses = Losses.From(request.Losses)
        };
    }
}
