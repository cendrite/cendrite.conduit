using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Device.Commands.ApproveLink;

public sealed record ApproveLinkCommand : ICommand
{
    public required Guid PendingLinkId { get; init; }
    public required string Approval { get; init; }
}
