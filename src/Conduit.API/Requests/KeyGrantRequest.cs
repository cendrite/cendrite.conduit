namespace Conduit.API.Requests;

internal sealed record KeyGrantRequest(byte[] PublicKey, int Role);
