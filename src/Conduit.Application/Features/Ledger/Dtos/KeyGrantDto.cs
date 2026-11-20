namespace Conduit.Application.Features.Ledger.Dtos;

public sealed record KeyGrantDto(byte[] PublicKey, int Role);
