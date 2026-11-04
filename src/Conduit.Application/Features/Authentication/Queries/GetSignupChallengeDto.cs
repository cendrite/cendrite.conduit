namespace Conduit.Application.Features.Authentication.Queries;

public sealed record GetSignupChallengeDto
{
    public required Guid ChallengeId { get; init; }
    public required string Challenge { get; init; }
    public required int Difficulty { get; init; }
    public required DateTime ExpiresAt { get; init; }
}
