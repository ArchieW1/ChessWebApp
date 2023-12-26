namespace ChessWebApp.ChessEngine.BoardLib;

public sealed partial class Board
{
    public static class Utils
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
    }
}