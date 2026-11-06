using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Device.Commands.RejectLink;

public sealed class RejectLinkCommandHandler(IIdentityUserProvider provider) : ICommandHandler<RejectLinkCommand>
{
    public async ValueTask<Result> Handle(RejectLinkCommand cmd, CancellationToken ct)
        => await provider.RejectLinkAsync(cmd.PendingLinkId, ct);
}
