namespace ChessWebApp.ChessEngine;

public static class AllianceExtension
{
    public static int GetDirection(this Alliance alliance)
    {
        return alliance switch
        {
            Alliance.White => -1,
            Alliance.Black => 1,
            _ => 1
        };
    }
}