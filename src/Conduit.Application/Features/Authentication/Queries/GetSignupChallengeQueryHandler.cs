using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Authentication.Queries;

public sealed class GetSignupChallengeQueryHandler(IIdentityProvider idProvider) : IQueryHandler<GetSignupChallengeQuery, GetSignupChallengeDto>
{
    public async ValueTask<Result<GetSignupChallengeDto>> Handle(GetSignupChallengeQuery q, CancellationToken ct)
        => await idProvider.GetSignupChallengeAsync(ct);
}
