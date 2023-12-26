using ChessWebApp.ChessEngine;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ChessWebApp.UI.Components;

public sealed partial class Board : ComponentBase
{
    [Inject]
    private IBoardService BoardService { get; set; } = default!;

    [Parameter]
    public bool Reverse { get; set; }
    
    [Parameter]
    public bool AiGame { get; set; }
    
    [Parameter]
    public string Difficulty { get; set; } = string.Empty;
    
    private string ReversedCss => Reverse ? "-reverse" : "";
    
    protected override void OnInitialized()
    {
        BoardService.IsAiGame = AiGame;
    }
    
    private static bool IsPieceBoxWhite(int i)
    {
        return (i + i / 8) % 2 == 0;
    }

    private bool IsTileSelected(Tile tile)
    {
        return BoardService.SourceTile is not null &&
               BoardService.SourceTile.TileCoordinate == tile.TileCoordinate;
    }

    private bool IsTileLegalMove(Tile tile)
    {
        if (BoardService.SourceTile is null)
        {
            return false;
        }

        if (BoardService.IsAiGame && BoardService.Board.CurrentPlayer.Alliance != Alliance.White)
        {
            return false;
        }
        
        foreach (Move move in BoardService.Board.CurrentPlayer.LegalMoves)
        {
            if (tile.TileCoordinate == move.DestinationCoordinate &&
                move.MovedPiece.Position == BoardService.SourceTile.Piece.Position)
            {
                return true;
            }
        }

        return false;
    }
}