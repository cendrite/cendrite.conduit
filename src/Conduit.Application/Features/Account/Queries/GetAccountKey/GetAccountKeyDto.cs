namespace Conduit.Application.Features.Account.Queries.GetAccountKey;

public sealed record GetAccountKeyDto
{
    public required string AccountPublicKey { get; init; }
}
