using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Endpoints;
using FastEndpoints;

namespace ChessWebApp.Api.Summaries;

public sealed class GetAllUsersSummary : Summary<GetAllUsersEndpoint>
{
    public GetAllUsersSummary()
    {
        Summary = "Returns all the users in the system";
        Description = "Returns all the users in the system";
        Response<GetAllUsersResponse>(200, "All users in the system are returned");
    }
}
