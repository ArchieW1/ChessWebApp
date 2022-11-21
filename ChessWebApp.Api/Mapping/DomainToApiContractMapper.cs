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
}
