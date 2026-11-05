namespace Conduit.Application.Features.Account.Commands.Recover;

public sealed record RecoverDto
{
    public required Guid UserId { get; init; }
    public required Guid DeviceId { get; init; }
    public required Guid DeviceConnectionId { get; init; }
}
