using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Endpoints;
using FastEndpoints;

namespace ChessWebApp.Api.Summaries;

public sealed class GetUserSummary : Summary<GetUserEndpoint>
{
    public GetUserSummary()
    {
        Summary = "Returns a single user by id";
        Description = "Returns a single user by id";
        Response<GetAllUsersResponse>(200, "Successfully found and returned the user");
        Response(404, "The user does not exist in the system");
    }
}
