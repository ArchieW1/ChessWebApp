using ChessWebApp.Api.Endpoints;
using FastEndpoints;

namespace ChessWebApp.Api.Summaries;

public sealed class DeleteUserSummary : Summary<DeleteUserEndpoint>
{
    public DeleteUserSummary()
    {
        Summary = "Deleted a user the system";
        Description = "Deleted a user the system";
        Response(204, "The user was deleted successfully");
        Response(404, "The user was not found in the system");
    }
}
