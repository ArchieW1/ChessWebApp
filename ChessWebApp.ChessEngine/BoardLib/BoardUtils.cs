namespace ChessWebApp.ChessEngine.BoardLib;

public static class BoardUtils
{
    public enum Row
    {
        First = 0, 
        Second, 
        Third, 
        Forth, 
        Fifth, 
        Sixth, 
        Seventh, 
        Eighth
    }
    
    public enum Column
    {
        First = 0, 
        Second, 
        Third, 
        Forth, 
        Fifth, 
        Sixth, 
        Seventh, 
        Eighth
    }
    
    public const int NumberOfTiles = 64;
    public const int NumberOfColumns = 8;
    public const int NumberOfRows = 8;

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