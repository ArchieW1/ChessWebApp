namespace ChessWebApp.ChessEngine.BoardLib;

public static class BoardExtensions
{
    public static bool IsValid(this int coordinate)
    {
        return coordinate is >= 0 and < Board.Utils.NumberOfTiles;
    }

    public static Board.Utils.Column ToColumn(this int coordinate)
    {
        return (Board.Utils.Column)(coordinate / Board.Utils.NumberOfColumns);
    }
    
    public static Board.Utils.Row ToRow(this int coordinate)
    {
        return (Board.Utils.Row)(coordinate % Board.Utils.NumberOfRows);
    }
}