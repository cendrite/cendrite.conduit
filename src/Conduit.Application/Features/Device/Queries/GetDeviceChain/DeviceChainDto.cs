namespace Conduit.Application.Features.Device.Queries.GetDeviceChain;

public sealed record DeviceChainDto
{
    public required Guid UserId { get; init; }
    public required string AccountPublicKey { get; init; }
    public required IReadOnlyList<ChainDeviceDto> Devices { get; init; } = [];
    public required IReadOnlyList<ChainRotationDto> Rotations { get; init; } = [];
}
