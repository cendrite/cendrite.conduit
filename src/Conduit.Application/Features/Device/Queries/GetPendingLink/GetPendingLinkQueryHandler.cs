using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Device.Queries.GetPendingLink;

public sealed class GetPendingLinkQueryHandler(IIdentityUserProvider provider) : IQueryHandler<GetPendingLinkQuery, PendingLinkDto>
{
    public async ValueTask<Result<PendingLinkDto>> Handle(GetPendingLinkQuery q, CancellationToken ct)
        => await provider.GetPendingLinkAsync(q.PendingLinkId, ct);
}
