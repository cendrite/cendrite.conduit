using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Device.Queries.GetDeviceChain;

public sealed record GetDeviceChainQuery : IQuery<DeviceChainDto>
{
    public required Guid ConnectionId { get; init; }
}
