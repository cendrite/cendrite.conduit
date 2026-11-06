namespace Conduit.API.Requests;

public sealed record ApproveLinkRequest
{
    public required string Approval { get; init; }
}
