namespace ChessWebApp.ChessEngine.BoardLib;

public static class BoardUtils
{
    public const int NumberOfTiles = 64;
    public const int NumberOfColumns = 8;
    public const int NumberOfRows = 8;

    public static bool IsValidCoordinate(int coordinate)
    {
        return coordinate is >= 0 and < NumberOfTiles;
    }

    public static Column CoordinatesColumn(int coordinate)
    {
        return (Column)(coordinate / NumberOfColumns);
    }
    
    public static Row CoordinatesRow(int coordinate)
    {
        return (Row)(coordinate % NumberOfRows);
    }
}