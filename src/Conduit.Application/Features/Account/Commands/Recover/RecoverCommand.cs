using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Account.Commands.Recover;

public sealed record RecoverCommand : ICommand<RecoverDto>
{
    public required string AccountPublicKey { get; init; }
    public required Guid ChallengeId { get; init; }
    public required string ChallengeSignature { get; init; }
    public required string DevicePublicKey { get; init; }
    public required string DeviceApproval { get; init; }
    public required string DeviceAppVersion { get; init; }
    public required int DeviceType { get; init; }
    public required int DevicePlatform { get; init; }
}
