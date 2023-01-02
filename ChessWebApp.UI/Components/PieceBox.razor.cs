using ChessWebApp.ChessEngine;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.ChessEngine.PieceLib;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ChessWebApp.UI.Components;

public sealed partial class PieceBox : ComponentBase
{
    [Parameter]
    public bool IsWhite { get; set; }

    [Parameter]
    public Tile Tile { get; set; } = default!;

    [Parameter]
    public bool IsSelected { get; set; }

    [Parameter]
    public EventCallback<string> OnUpdate { get; set; }
    
    [Parameter]
    public bool IsLegalMove { get; set; }

    private string PieceSymbol => Tile.Piece.ToString().ToLower();
    private string PieceColour => Tile.Piece.Alliance switch
    {
        Alliance.None => "",
        Alliance.White => "white",
        Alliance.Black => "black",
        _ => throw new ArgumentOutOfRangeException()
    };
    
    private string _colour = "";
    private string SelectedCss => IsSelected ? "selected-box" : "";
    private string LegalMoveCss => IsLegalMove ? "potential-box" : "";

    protected override void OnInitialized()
    {
        _colour = IsWhite ? "white" : "black";
    }

    private void HandleLeftClick()
    {
        if (_board.SourceTile is null)
        {
            SelectPiece();
        }
        else
        {
            MovePiece();
        }
        OnUpdate.InvokeAsync(null);
    }

    private void HandleRightClick()
    {
        UnSelectPiece();
        OnUpdate.InvokeAsync(null);
    }

    private void UnSelectPiece()
    {
        _board.SourceTile = null;
        _board.DestinationTile = null;
        _board.HumanMovedPiece = null;
    }

    private void SelectPiece()
    {
        _board.SourceTile = Tile;
        _board.HumanMovedPiece = Tile.Piece;
        if (_board.HumanMovedPiece is Empty)
        {
            _board.SourceTile = null;
        }
    }

    private void MovePiece()
    {
        if (_board.SourceTile is null)
        {
            return;
        }
        
        _board.DestinationTile = Tile;
        Move move = Move.Builder.CreateMove(_board.Board, _board.SourceTile.TileCoordinate,
            _board.DestinationTile.TileCoordinate);
        MoveTransition transition = _board.Board.CurrentPlayer.MakeMove(move);
        if (transition.MoveStatus == MoveStatus.Done)
        {
            _board.Board = transition.Board;
        }

        UnSelectPiece();
    }
}