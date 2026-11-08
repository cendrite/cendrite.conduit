namespace Conduit.Application.Features.Device.Queries.GetDeviceChain;

public sealed record ChainDeviceDto
{
    public required Guid Id { get; init; }
    public required Guid ConnectionId { get; init; }
    public required string PublicKey { get; init; }
    public required string SignalIdentityKey { get; init; }
    public string? ApprovedBy { get; init; }
    public string? ApprovalSignature { get; init; }
    public required bool Removed { get; init; }
}
