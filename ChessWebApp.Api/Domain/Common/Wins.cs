using ValueOf;

namespace ChessWebApp.Api.Domain.Common;

public sealed class Wins : ValueOf<int, Wins>
{
    protected override void Validate()
    {
        if (Value < 0)
        {
            throw new ArgumentException("Wins cannot be less than 0.", nameof(Wins));
        }
    }
}