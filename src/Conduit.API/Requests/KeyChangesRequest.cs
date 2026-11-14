namespace Conduit.API.Requests;

internal sealed record KeyChangesRequest(
    List<byte[]> Added,
    List<byte[]> InvitesAdded,
    List<byte[]> Removed);
