using static ChessWebApp.ChessEngine.BoardLib.BoardUtils;

namespace ChessWebApp.ChessEngine.BoardLib;

public static class BoardExtensions
{
    public static bool IsValid(this int coordinate)
    {
        return coordinate is >= 0 and < NumberOfTiles;
    }

    public static Column GetColumn(this int coordinate)
    {
        return (Column)(coordinate / NumberOfColumns);
    }
    
    public static Row GetRow(this int coordinate)
    {
        return (Row)(coordinate % NumberOfRows);
    }
}