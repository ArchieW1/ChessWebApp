using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;
using Microsoft.AspNetCore.Components;

namespace ChessWebApp.UI.Components;

public sealed partial class Board : ComponentBase
{
    [Parameter]
    public bool Reverse { get; set; }
    
    private string ReversedCss => Reverse ? "-reverse" : "";
    
    private static bool IsPieceBoxWhite(int i)
    {
        return (i + i / 8) % 2 == 0;
    }

    private bool IsTileSelected(Tile tile)
    {
        return _board.SourceTile is not null &&
               _board.SourceTile.TileCoordinate == tile.TileCoordinate;
    }

    private bool IsTileLegalMove(Tile tile)
    {
        if (_board.SourceTile is null)
        {
            return false;
        }

        if (_board.SourceTile.Piece.Alliance != _board.Board.CurrentPlayer.Alliance)
        {
            return false;
        }

        foreach (Move move in _board.SourceTile.Piece.CalculateLegalMoves(_board.Board))
        {
            if (tile.TileCoordinate == move.DestinationCoordinate)
            {
                return true;
            }
        }

        return false;
    }
}