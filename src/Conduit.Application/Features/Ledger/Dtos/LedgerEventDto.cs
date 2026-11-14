namespace Conduit.Application.Features.Ledger.Dtos;

public sealed record LedgerEventDto
{
    public required int Version { get; init; }
    public required byte[] PreviousHash { get; init; }
    public required byte[] Payload { get; init; }
    public required byte[] WriteKeyPublic { get; init; }
    public required byte[] Signature { get; init; }
    public required KeyChangesDto KeyChanges { get; init; }
}
