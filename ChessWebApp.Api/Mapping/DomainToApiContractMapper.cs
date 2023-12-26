using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Domain;

namespace ChessWebApp.Api.Mapping;

public static class DomainToApiContractMapper
{
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse
        {
            Email = user.Email.Value,
            Username = user.Username.Value,
            Wins = user.Wins.Value,
            Losses = user.Losses.Value
        };
    }
    
    public static GetUserLoginResponse ToUserLoginResponse(this User user)
    {
        return new GetUserLoginResponse
        {
            Email = user.Email.Value,
            Username = user.Username.Value
        };
    }
    
    public static GetUserStatsResponse ToUserStatsResponse(this User user)
    {
        return new GetUserStatsResponse
        {
            Username = user.Username.Value,
            Wins = user.Wins.Value,
            Losses = user.Losses.Value
        };
    }

    public static GetAllUsersResponse ToUsersResponse(this IEnumerable<User> users)
    {
        return new GetAllUsersResponse
        {
            Users = users.Select(user => new UserResponse
            {
                Email = user.Email.Value,
                Username = user.Username.Value,
                Wins = user.Wins.Value,
                Losses = user.Losses.Value
            })
        };
    }

    public static GetAllUsersLoginResponse ToUsersLoginResponse(this IEnumerable<User> users)
    {
        return new GetAllUsersLoginResponse
        {
            Users = users.Select(user => new GetUserLoginResponse
            {
                Email = user.Email.Value,
                Username = user.Username.Value
            })
        };
    }
    
    public static GetAllUsersStatsResponse ToUsersStatsResponse(this IEnumerable<User> users)
    {
        return new GetAllUsersStatsResponse
        {
            Users = users.Select(user => new GetUserStatsResponse
            {
                Username = user.Username.Value,
                Wins = user.Wins.Value,
                Losses = user.Losses.Value
            })
        };
    }
}
