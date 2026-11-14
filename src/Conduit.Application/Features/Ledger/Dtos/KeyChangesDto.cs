namespace Conduit.Application.Features.Ledger.Dtos;

public sealed record KeyChangesDto(
    IReadOnlyCollection<byte[]> Added,
    IReadOnlyCollection<byte[]> InvitesAdded,
    IReadOnlyCollection<byte[]> Removed);
