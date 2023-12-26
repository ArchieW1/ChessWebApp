namespace ChessWebApp.Api.Contracts.Responses;

public sealed class GetWinRateResponse
{
    public double WinRate { get; set; }
}

public sealed class GetFavouriteMoveResponse
{
    public string Move { get; set; } = default!;
}