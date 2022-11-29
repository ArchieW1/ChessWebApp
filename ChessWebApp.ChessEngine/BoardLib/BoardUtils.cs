namespace ChessWebApp.ChessEngine.BoardLib;

public static class BoardUtils
{
    public static bool IsValidCoordinate(int coordinate)
    {
        return coordinate is >= 0 and < 64;
    }

    public static Column CoordinatesColumn(int coordinate)
    {
        return (Column)(coordinate / 8);
    }
    
    public static Row CoordinatesRow(int coordinate)
    {
        return (Row)(coordinate % 8);
    }
}