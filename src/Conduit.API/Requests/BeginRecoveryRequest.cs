namespace Conduit.API.Requests;

public sealed record BeginRecoveryRequest
{
    public required string AccountPublicKey { get; init; }
}
