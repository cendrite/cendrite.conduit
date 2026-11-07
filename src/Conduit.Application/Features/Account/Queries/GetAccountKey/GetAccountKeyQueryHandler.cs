using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Account.Queries.GetAccountKey;

public sealed class GetAccountKeyQueryHandler(IIdentityUserProvider identityUserProvider) : IQueryHandler<GetAccountKeyQuery, GetAccountKeyDto>
{
    public async ValueTask<Result<GetAccountKeyDto>> Handle(GetAccountKeyQuery request, CancellationToken cancellationToken)
    {
        return await identityUserProvider.GetAccountKeyAsync(cancellationToken);
    }
}
