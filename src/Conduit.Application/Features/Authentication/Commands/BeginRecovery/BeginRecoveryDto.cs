namespace Conduit.Application.Features.Authentication.Commands.BeginRecovery;

public sealed record BeginRecoveryDto
{
    public required Guid ChallengeId { get; init; }
    public required string Challenge { get; init; }
    public required DateTime ExpiresAt { get; init; }
}
