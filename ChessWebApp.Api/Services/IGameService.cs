namespace ChessWebApp.Api.Services;

public interface IGameService
{
    public Task<string> GetFavouriteMove(string player);

    public Task<double> GetUserWinRate(string player);
}