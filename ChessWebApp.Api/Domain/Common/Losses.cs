using ValueOf;

namespace ChessWebApp.Api.Domain.Common;

public sealed class Losses : ValueOf<int, Losses>
{
    protected override void Validate()
    {
        if (Value < 0)
        {
            throw new ArgumentException("Losses cannot be less than 0.", nameof(Losses));
        }
    }
}