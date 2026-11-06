using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Device.Commands.RejectLink;

public sealed record RejectLinkCommand : ICommand
{
    public required Guid PendingLinkId { get; init; }
}
