using Conduit.Application.Features.Keys.Commands.Create;
using Conduit.Application.Features.Ledger.Dtos;

namespace Conduit.API.Requests;

internal static class RequestExtensions
{
    public static CreateKeyBundleCommand MapToCommand(this CreateKeyBundleRequest req) =>
        new()
        {
            AccessKey = req.AccessKey,
            IdentityKey = req.IdentityKey,
            RegistrationId = req.RegistrationId,
            SignedPrekeyId = req.SignedPrekeyId,
            SignedPreKeyPublic = req.SignedPreKeyPublic,
            SignedPreKeySignature = req.SignedPreKeySignature,
            PreKeys = req.PreKeys,
            SignedPreKeyTimestamp = req.SignedPreKeyTimestamp,
            KyberPreKeyId = req.KyberPreKeyId,
            KyberPreKeyPublic = req.KyberPreKeyPublic,
            KyberPreKeySignature = req.KyberPreKeySignature
        };

    public static KeyChangesDto MapToDto(this KeyChangesRequest req) =>
        new(
            req.Added,
            [.. req.InvitesAdded.Select(g => new KeyGrantDto(g.PublicKey, g.Role))],
            req.Removed,
            [.. req.RolesChanged.Select(g => new KeyGrantDto(g.PublicKey, g.Role))]);
}
