using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Device.Commands.RedeemLinkCode;

public sealed class RedeemLinkCodeCommandHandler(IIdentityUserProvider provider) : ICommandHandler<RedeemLinkCodeCommand, RedeemLinkCodeDto>
{
    public async ValueTask<Result<RedeemLinkCodeDto>> Handle(RedeemLinkCodeCommand cmd, CancellationToken ct)
        => await provider.RedeemLinkCodeAsync(cmd, ct);
}
