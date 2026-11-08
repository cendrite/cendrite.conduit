namespace Conduit.API.Requests;

public sealed record SignupRequest
{
    public required string Username { get; init; }
    public required string AccountPublicKey { get; init; }
    public required Guid ChallengeId { get; init; }
    public required ulong PowNonce { get; init; }
    public required string ChallengeSignature { get; init; }
    public required string DevicePublicKey { get; init; }
    public required string SignalIdentityKey { get; init; }
    public required string DeviceApproval { get; init; }
    public required string DeviceAppVersion { get; init; }
    public required int DeviceType { get; init; }
    public required int DevicePlatform { get; init; }
}
