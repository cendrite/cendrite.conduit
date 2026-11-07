using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Account.Commands.RotateAccountKey;

public sealed class RotateAccountKeyCommandHandler(IIdentityUserProvider identityUserProvider) : ICommandHandler<RotateAccountKeyCommand>
{
    public async ValueTask<Result> Handle(RotateAccountKeyCommand request, CancellationToken cancellationToken)
    {
        return await identityUserProvider.RotateAccountKeyAsync(request, cancellationToken);
    }
}
