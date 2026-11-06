using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Device.Queries.GetPendingLink;

public sealed record GetPendingLinkQuery : IQuery<PendingLinkDto>
{
    public required Guid PendingLinkId { get; init; }
}
