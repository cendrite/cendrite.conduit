using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Device.Commands.Rename;

public sealed class RenameDeviceCommandHandler(IIdentityUserProvider identityUserProvider) : ICommandHandler<RenameDeviceCommand>
{
    public async ValueTask<Result> Handle(RenameDeviceCommand request, CancellationToken cancellationToken)
    {
        return await identityUserProvider.RenameDeviceAsync(request.TargetDeviceId, request.Name, cancellationToken);
    }
}
