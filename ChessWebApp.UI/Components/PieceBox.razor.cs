using ChessWebApp.ChessEngine;
using ChessWebApp.ChessEngine.AiLib;
using ChessWebApp.ChessEngine.BoardLib;
using static ChessWebApp.ChessEngine.BoardLib.Board.Utils;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.ChessEngine.PieceLib;
using ChessWebApp.ChessEngine.PlayerLib;
using ChessWebApp.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ChessWebApp.UI.Components;

public sealed partial class PieceBox : ComponentBase
{
    [Inject]
    private IBoardService BoardService { get; set; } = default!;
    
    [Parameter]
    public bool IsWhite { get; set; }

    [Parameter]
    public Tile Tile { get; set; } = default!;

    [Parameter]
    public bool IsSelected { get; set; }
    
    [Parameter]
    public IMoveStrategy? MoveStrategy { get; set; }
    
    [Parameter]
    public string Difficulty { get; set; } = default!;
    
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


    private string CheckCss => CurrentPlayerIsInCheck() ? "check-box" : "";

    private bool CurrentPlayerIsInCheck()
    {
        Player currentPlayer = BoardService.Board.CurrentPlayer;
        return currentPlayer.IsInCheck && currentPlayer.Alliance == Tile.Piece.Alliance && Tile.Piece is King;
    }

    private string _colour = "";
    private string SelectedCss => IsSelected ? "selected-box" : "";
    private string LegalMoveCss => IsLegalMove ? "potential-box" : "";
    private bool _isIllegalMove = false;
    private string IllegalMoveCss => _isIllegalMove ? "not-allowed-box" : "";
    
    protected override void OnInitialized()
    {
        _colour = IsWhite ? "white" : "black";
        MoveStrategy = Difficulty switch
        {
            "easy" => new MiniMax(new StandardBoardEvaluator(), 1),
            "medium" => new MiniMax(new StandardBoardEvaluator(), 2),
            "hard" => new MiniMax(new StandardBoardEvaluator(), 3),
            _ => new MiniMax(new StandardBoardEvaluator(), 1)
        };
    }

    protected override void OnAfterRender(bool firstRender)
    {
        _isIllegalMove = false;

        if (BoardService.IsAiGame && BoardService.Board.CurrentPlayer.IsInCheckmate)
        {
            BoardService.IsAiGame = false;
            
        }
        else if (BoardService.IsAiGame && BoardService.Board.CurrentPlayer.IsInStalemate)
        {
            BoardService.IsAiGame = false;
            
        }
    }

    private void HandleLeftClick()
    {
        _isIllegalMove = false;
        if (BoardService.SourceTile is null)
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
        BoardService.SourceTile = null;
        BoardService.DestinationTile = null;
        BoardService.HumanMovedPiece = null;
    }

    private void SelectPiece()
    {
        BoardService.SourceTile = Tile;
        BoardService.HumanMovedPiece = Tile.Piece;
        if (BoardService.HumanMovedPiece is Empty ||
            (BoardService.IsAiGame && BoardService.Board.CurrentPlayer.Alliance != Alliance.White))
        {
            BoardService.SourceTile = null;
        }
    }

    private void MovePiece()
    {
        if (BoardService.SourceTile is null)
        {
            return;
        }
        
        BoardService.DestinationTile = Tile;
        Move move = Move.Builder.CreateMove(BoardService.Board, BoardService.SourceTile.TileCoordinate,
            BoardService.DestinationTile.TileCoordinate);
        MoveTransition transition = BoardService.Board.CurrentPlayer.MakeMove(move);
        switch (transition.MoveStatus)
        {
            case MoveStatus.Done:
                BoardService.Board = transition.Move.Execute();
                if (BoardService.IsAiGame)
                {
                    Move aiMove = MoveStrategy!.Execute(BoardService.Board);
                    BoardService.Board = aiMove.Execute();
                }
                break;
            case MoveStatus.Illegal:
                break;
            case MoveStatus.LeavesPlayerInCheck:
                if (BoardService.Board.CurrentPlayer.IsInCheck)
                {
                    BoardService.Board = transition.Move.UnExecute(Tile.Piece);
                    _isIllegalMove = true;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        UnSelectPiece();
    }
}