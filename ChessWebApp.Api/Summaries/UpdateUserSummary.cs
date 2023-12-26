using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Endpoints;
using FastEndpoints;

namespace ChessWebApp.Api.Summaries;

public sealed class UpdateUserSummary : Summary<UpdateUserEndpoint>
{
    public UpdateUserSummary()
    {
        Summary = "Updates an existing user in the system";
        Description = "Updates an existing user in the system";
        Response<UserResponse>(201, "User was successfully updated");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}

public sealed class UpdateUserLoginSummary : Summary<UpdateUserLoginEndpoint>
{
    public UpdateUserLoginSummary()
    {
        Summary = "Updates an existing users login information in the system";
        Description = "Updates an existing users login information in the system";
        Response<UserResponse>(201, "User was successfully updated");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}


public sealed class UpdateUserStatsSummary : Summary<UpdateUserStatsEndpoint>
{
    public UpdateUserStatsSummary()
    {
        Summary = "Updates an existing users statistics information in the system";
        Description = "Updates an existing users statistics information in the system";
        Response<UserResponse>(201, "User was successfully updated");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
