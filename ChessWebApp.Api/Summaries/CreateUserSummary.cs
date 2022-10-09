using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Endpoints;
using FastEndpoints;

namespace ChessWebApp.Api.Summaries;

public sealed class CreateUserSummary : Summary<CreateUserEndpoint>
{
    public CreateUserSummary()
    {
        Summary = "Creates a new user in the system";
        Description = "Creates a new user in the system";
        Response<UserResponse>(201, "User was successfully created");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
