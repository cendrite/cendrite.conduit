namespace Conduit.Application.Features.Ledger.Dtos;

public sealed record KeyChangesDto(
    IReadOnlyCollection<byte[]> Added,
    IReadOnlyCollection<KeyGrantDto> InvitesAdded,
    IReadOnlyCollection<byte[]> Removed,
    IReadOnlyCollection<KeyGrantDto> RolesChanged);
