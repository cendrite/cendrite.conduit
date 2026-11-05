using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Authentication.Commands.BeginRecovery;

public sealed record BeginRecoveryCommand : ICommand<BeginRecoveryDto>
{
    public required string AccountPublicKey { get; init; }
}
