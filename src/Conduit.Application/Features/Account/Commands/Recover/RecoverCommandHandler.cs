using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Account.Commands.Recover;

public sealed class RecoverCommandHandler(IIdentityUserProvider identityUserProvider) : ICommandHandler<RecoverCommand, RecoverDto>
{
    public async ValueTask<Result<RecoverDto>> Handle(RecoverCommand request, CancellationToken cancellationToken)
    {
        return await identityUserProvider.RecoverAsync(request, cancellationToken);
    }
}
