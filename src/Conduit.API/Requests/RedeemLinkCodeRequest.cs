namespace Conduit.API.Requests;

public sealed record RedeemLinkCodeRequest
{
    public required string Code { get; init; }
    public required string DevicePublicKey { get; init; }
    public required string SignalIdentityKey { get; init; }
    public required string DeviceAppVersion { get; init; }
    public required int DeviceType { get; init; }
    public required int DevicePlatform { get; init; }
}
