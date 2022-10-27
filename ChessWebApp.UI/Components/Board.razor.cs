using Microsoft.AspNetCore.Components;

namespace ChessWebApp.UI.Components;

public sealed partial class Board : ComponentBase
{
    [Parameter]
    public bool Reverse { get; set; }
    
    private string GetReversed => Reverse ? "-reverse" : "";
    
    private int _count = 0;
    private bool IsPieceBoxWhite(int i)
    {
        if (i % 8 == 0)
        {
            _count++;
        }

        if (_count % 2 == 0)
        {
            return i % 2 != 0;
        }
        
        return i % 2 == 0;
    }
}