using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Device.Commands.CreateLinkCode;

public sealed class CreateLinkCodeCommandHandler(IIdentityUserProvider provider) : ICommandHandler<CreateLinkCodeCommand, CreateLinkCodeDto>
{
    public async ValueTask<Result<CreateLinkCodeDto>> Handle(CreateLinkCodeCommand cmd, CancellationToken ct)
        => await provider.CreateLinkCodeAsync(ct);
}
