using ValueOf;

namespace ChessWebApp.Api.Domain.Common;

public sealed class UserId : ValueOf<Guid, UserId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("User Id cannot be empty", nameof(UserId));
        }
    }
}
