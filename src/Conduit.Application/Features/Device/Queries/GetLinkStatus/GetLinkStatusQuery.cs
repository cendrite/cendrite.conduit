using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Device.Queries.GetLinkStatus;

public sealed record GetLinkStatusQuery : IQuery<LinkStatusDto>
{
    public required Guid PendingLinkId { get; init; }
    public required string PollSecret { get; init; }
}
