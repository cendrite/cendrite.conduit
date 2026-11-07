namespace Conduit.API.Requests;

public sealed record RotateAccountKeyRequest
{
    public required string NewAccountPublicKey { get; init; }
    public required string Proof { get; init; }
    public required string Approval { get; init; }
}
