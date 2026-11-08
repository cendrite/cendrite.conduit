namespace Conduit.Application.Features.Device.Queries.GetDeviceChain;

public sealed record ChainRotationDto
{
    public required Guid DeviceId { get; init; }
    public required string OldAccountPublicKey { get; init; }
    public required string NewAccountPublicKey { get; init; }
    public required string Proof { get; init; }
    public required string Approval { get; init; }
    public required DateTime Created { get; init; }
}
