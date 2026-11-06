namespace Conduit.Application.Features.Device.Commands.RedeemLinkCode;

public sealed record RedeemLinkCodeDto
{
    public required Guid PendingLinkId { get; init; }
    public required string PollSecret { get; init; }
    public required DateTime ExpiresAt { get; init; }
}
