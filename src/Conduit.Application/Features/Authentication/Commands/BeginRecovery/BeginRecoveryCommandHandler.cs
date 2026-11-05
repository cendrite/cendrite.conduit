using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Authentication.Commands.BeginRecovery;

public sealed class BeginRecoveryCommandHandler(IIdentityProvider idProvider) : ICommandHandler<BeginRecoveryCommand, BeginRecoveryDto>
{
    public async ValueTask<Result<BeginRecoveryDto>> Handle(BeginRecoveryCommand cmd, CancellationToken ct)
        => await idProvider.BeginRecoveryAsync(cmd.AccountPublicKey, ct);
}
