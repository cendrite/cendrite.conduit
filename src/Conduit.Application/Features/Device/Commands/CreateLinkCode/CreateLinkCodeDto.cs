namespace Conduit.Application.Features.Device.Commands.CreateLinkCode;

public sealed record CreateLinkCodeDto
{
    public required string Code { get; init; }
    public required DateTime ExpiresAt { get; init; }
}
