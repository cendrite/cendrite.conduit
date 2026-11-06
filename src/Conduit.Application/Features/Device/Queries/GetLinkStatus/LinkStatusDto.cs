namespace Conduit.Application.Features.Device.Queries.GetLinkStatus;

public sealed record LinkStatusDto
{
    public required string Status { get; init; }
    public Guid? UserId { get; init; }
    public Guid? DeviceId { get; init; }
    public Guid? DeviceConnectionId { get; init; }
}
