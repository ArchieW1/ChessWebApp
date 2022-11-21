using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Endpoints;
using FastEndpoints;

namespace ChessWebApp.Api.Summaries;

public sealed class LoginUserSummary : Summary<LoginUserEndpoint>
{
    public LoginUserSummary()
    {
        Summary = "Returns web token on successful login information.";
        Description = "Returns a web token.";
        Response<LoginUserResponse>(200, "Successfully generated and returned web token.");
        Response(404, "The user does not exist in the system");
        Response(401, "Password was invalid.");
    }
}