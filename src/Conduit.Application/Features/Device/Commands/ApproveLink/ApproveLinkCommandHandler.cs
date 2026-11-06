using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Device.Commands.ApproveLink;

public sealed class ApproveLinkCommandHandler(IIdentityUserProvider provider) : ICommandHandler<ApproveLinkCommand>
{
    public async ValueTask<Result> Handle(ApproveLinkCommand cmd, CancellationToken ct)
        => await provider.ApproveLinkAsync(cmd.PendingLinkId, cmd.Approval, ct);
}
