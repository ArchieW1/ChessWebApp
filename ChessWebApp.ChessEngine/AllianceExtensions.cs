using ChessWebApp.ChessEngine.PlayerLib;

namespace ChessWebApp.ChessEngine;

public static class AllianceExtensions
{
    public static int GetDirection(this Alliance alliance)
    {
        return alliance switch
        {
            Alliance.White => -1,
            Alliance.Black => 1,
            _ => 0
        };
    }

    public static Player ChoosePlayer(this Alliance alliance, Player whitePlayer, Player blackPlayer)
    {
        return alliance switch
        {
            Alliance.White => whitePlayer,
            Alliance.Black => blackPlayer,
            _ => throw new IndexOutOfRangeException()
        };
    }
}