namespace Conduit.API.Requests;

internal sealed record KeyChangesRequest(
    List<byte[]> Added,
    List<KeyGrantRequest> InvitesAdded,
    List<byte[]> Removed,
    List<KeyGrantRequest> RolesChanged);
