using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Device.Queries.GetDeviceChain;

public sealed class GetDeviceChainQueryHandler(IIdentityUserProvider provider) : IQueryHandler<GetDeviceChainQuery, DeviceChainDto>
{
    public async ValueTask<Result<DeviceChainDto>> Handle(GetDeviceChainQuery q, CancellationToken ct)
        => await provider.GetDeviceChainAsync(q.ConnectionId, ct);
}
