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

public sealed class GetAllUsersLoginSummary : Summary<GetAllUsersLoginEndpoint>
{
    public GetAllUsersLoginSummary()
    {
        Summary = "Returns all the users logins in the system";
        Description = "Returns all the users logins in the system";
        Response<GetAllUsersResponse>(200, "All users in the system are returned");
    }
}

public sealed class GetAllUsersStatsSummary : Summary<GetAllUsersStatsEndpoint>
{
    public GetAllUsersStatsSummary()
    {
        Summary = "Returns all the users statistics in the system";
        Description = "Returns all the users statistics in the system";
        Response<GetAllUsersResponse>(200, "All users in the system are returned");
    }
}
