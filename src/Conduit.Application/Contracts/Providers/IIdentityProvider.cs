using Conduit.Application.Features.Authentication.Commands.VerifyNonce;
using Conduit.Application.Features.Authentication.Queries;
using Conduit.Domain.Common;

namespace Conduit.Application.Contracts.Providers;

public interface IIdentityProvider
{
    public Task<Result> LogoutAsync(CancellationToken ct = default);
    public Task<Result<GetNonceDto>> GetNonceAsync(Guid userId, Guid deviceId, CancellationToken ct = default);
    public Task<Result<GetSignupChallengeDto>> GetSignupChallengeAsync(CancellationToken ct = default);
    public Task<Result<VerifyNonceDto>> VerifyNonceAsync(Guid userId, Guid nonceId, Guid deviceId, string nonce, bool announce, IReadOnlyList<string> tokenRequests, CancellationToken ct = default);
}
