namespace Conduit.Application.Features.Device.Queries.GetPendingLink;

public sealed record PendingLinkDto
{
    public required Guid PendingLinkId { get; init; }
    public required string DevicePublicKey { get; init; }
    public required string DevicePlatform { get; init; }
    public required string AccountPublicKey { get; init; }
    public required DateTime ExpiresAt { get; init; }
}
