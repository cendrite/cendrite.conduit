using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Device.Queries.GetLinkStatus;

public sealed class GetLinkStatusQueryHandler(IIdentityUserProvider provider) : IQueryHandler<GetLinkStatusQuery, LinkStatusDto>
{
    public async ValueTask<Result<LinkStatusDto>> Handle(GetLinkStatusQuery q, CancellationToken ct)
        => await provider.GetLinkStatusAsync(q.PendingLinkId, q.PollSecret, ct);
}
